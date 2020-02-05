using PlayBall.GroupManagement.Business.Models;
using PlayBall.GroupManagement.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlayBall.GroupManagement.Business.Impl.Services
{
    public class InMemoryGroupsService : IGroupsService
    {
        private static readonly Random randomGenerator = new Random();
        private readonly List<Group> _groups = new List<Group>();
        private long _currentId = 0;

        public Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct)
        {
            //await Task.Delay(100).ConfigureAwait(false);
            //throw new NotImplementedException();
            return Task.FromResult<IReadOnlyCollection<Group>>(_groups.AsReadOnly());
        }

        public async Task<Group> GetByIdAsync(long id, CancellationToken ct)
        {
            await Task.Delay(1000, ct);
            var extResult1Task = CallExternalServiceAsync(1,ct);
            var extResult2Task = CallExternalServiceAsync(2,ct);

            await Task.WhenAll(extResult1Task, extResult2Task);

            return _groups.SingleOrDefault(g => g.Id == id);

        }

        public Task<Group> UpdateAsync(Group group, CancellationToken ct)
        {
            var toUpdate = _groups.SingleOrDefault(g => g.Id == group.Id);

            if (toUpdate == null)
            {
                return null;
            }

            toUpdate.Name = group.Name;
            return Task.FromResult(toUpdate);
        }

        public Task<Group> AddAsync(Group group, CancellationToken ct)
        {
            group.Id = ++_currentId;
            _groups.Add(group);
            return Task.FromResult(group);
        }

        private async Task<int> CallExternalServiceAsync(int multiplier, CancellationToken ct)
        {
            await Task.Delay(1000 * multiplier);
            return randomGenerator.Next();
        }

    }
}
