using System.Collections.Concurrent;
using System.Collections.Generic;
using MerchandaiseDomain.Models;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using Microsoft.Extensions.Primitives;

namespace MerchandaiseInfrastructure.Infrastructure
{
    public class ChangeTracker:IChangeTracker
    {
        public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();

        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField;

        public ChangeTracker()
        {
            _usedEntitiesBackingField = new ConcurrentBag<Entity>();
        }
        
        public void Track(Entity entity)
        {
            _usedEntitiesBackingField.Add(entity);
        }
    }
}