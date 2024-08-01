#nullable disable

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using N4CoreLite.Contexts.Bases;
using N4CoreLite.Records.Bases;
using System.Linq.Expressions;
using System.Reflection;

namespace N4CoreLite.Repositories.Bases
{
    public abstract class RepoBase<TEntity> : IDisposable where TEntity : Record, new()
	{
		protected readonly IDb _db;
		protected readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly PropertyInfo _guidProperty;
        protected readonly PropertyInfo _isDeletedProperty;
		protected readonly PropertyInfo _createDateProperty;
		protected readonly PropertyInfo _createdByProperty;
		protected readonly PropertyInfo _updateDateProperty;
		protected readonly PropertyInfo _updatedByProperty;

		protected bool _hasGuid => _guidProperty is not null;
		protected bool _hasIsDeleted => _isDeletedProperty is not null;
		protected bool _hasModifiedBy => _createDateProperty is not null && _createdByProperty is not null && _updateDateProperty is not null && _updatedByProperty is not null;

		public string Collation { get; set; } = "Turkish_CI_AS";

        protected RepoBase(IDb db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;

            _guidProperty = typeof(TEntity).GetProperty(nameof(Record.Guid));
            _isDeletedProperty = typeof(TEntity).GetProperty(nameof(ISoftDelete.IsDeleted));
            _createDateProperty = typeof(TEntity).GetProperty(nameof(IModifiedBy.CreateDate));
            _createdByProperty = typeof(TEntity).GetProperty(nameof(IModifiedBy.CreatedBy));
            _updateDateProperty = typeof(TEntity).GetProperty(nameof(IModifiedBy.UpdateDate));
            _updatedByProperty = typeof(TEntity).GetProperty(nameof(IModifiedBy.UpdatedBy));
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual IQueryable<TEntity> Query(bool isNoTracking = false)
		{
			var query = isNoTracking ? _db.Set<TEntity>().AsNoTracking() : _db.Set<TEntity>();
			if (_isDeletedProperty is not null)
				query = query.Where(q => (EF.Property<bool?>(q, _isDeletedProperty.Name) ?? false) == false);
            return query;
        }

		public virtual void Create(TEntity entity)
		{
			_db.Set<TEntity>().Add(entity);
			ApplyChanges();
        }

		public virtual void Update(TEntity entity)
		{
			_db.Set<TEntity>().Update(entity);
            ApplyChanges();
        }

		public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
		{
            _db.Set<TEntity>().RemoveRange(_db.Set<TEntity>().Where(predicate));
            ApplyChanges();
        }

		protected virtual void ApplyChanges()
		{
			if (_hasGuid || _hasIsDeleted || _hasModifiedBy)
			{
				foreach (var entry in _db.ChangeTracker.Entries<TEntity>())
				{
					switch (entry.State)
					{
						case EntityState.Added:
							if (_hasGuid)
								entry.CurrentValues[_guidProperty.Name] = Guid.NewGuid().ToString();
							if (_hasModifiedBy)
							{
								entry.CurrentValues[_createDateProperty.Name] = DateTime.Now;
								entry.CurrentValues[_createdByProperty.Name] = _httpContextAccessor.HttpContext?.User.Identity?.Name;
							}
							break;
						case EntityState.Modified:
                            if (_hasGuid)
                                entry.Property(_guidProperty.Name).IsModified = false;
                            if (UpdateChangesDetected(entry))
                            {
								if (_hasModifiedBy)
								{
									entry.CurrentValues[_updateDateProperty.Name] = DateTime.Now;
									entry.CurrentValues[_updatedByProperty.Name] = _httpContextAccessor.HttpContext?.User.Identity?.Name;
								}
                            }
                            else
                            {
                                entry.State = EntityState.Unchanged;
                            }
							break;
						case EntityState.Deleted:
							if (_hasIsDeleted)
							{
								entry.CurrentValues[_isDeletedProperty.Name] = true;
                                entry.State = EntityState.Modified;
                            }
							break;
                    }
				}
			}
		}

		protected virtual bool UpdateChangesDetected(EntityEntry<TEntity> entry)
		{
			return entry.Properties.Any(p => p.IsModified &&
				((p.CurrentValue is null && p.OriginalValue is not null) ||
				(p.CurrentValue is not null && p.OriginalValue is null) ||
				(p.CurrentValue is not null && p.OriginalValue is not null && !p.CurrentValue.Equals(p.OriginalValue))));
        }

		public void Dispose()
		{
			_db?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
