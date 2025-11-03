using Setup.Common;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task CreateAsync_ShouldAddComputer()
        {
            var service = new ComputerService();
            var computer = Computer.GenerateRandom();

            bool result = await service.CreateAsync(computer);
            var all = await service.ReadAllAsync();

            Assert.True(result);
            Assert.Contains(computer, all);
        }

        [Fact]
        public async Task ReadAsync_ShouldReturnComputerById()
        {
            var service = new ComputerService();
            var computer = Computer.GenerateRandom();
            await service.CreateAsync(computer);

            var result = await service.ReadAsync(computer.Id);

            Assert.NotNull(result);
            Assert.Equal(computer.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateComputer()
        {
            var service = new ComputerService();
            var computer = Computer.GenerateRandom();
            await service.CreateAsync(computer);

            computer.RAM += 8;
            bool updated = await service.UpdateAsync(computer);
            var result = await service.ReadAsync(computer.Id);

            Assert.True(updated);
            Assert.Equal(computer.RAM, result.RAM);
        }

        [Fact]
        public async Task RemoveAsync_ShouldDeleteComputer()
        {
            var service = new ComputerService();
            var computer = Computer.GenerateRandom();
            await service.CreateAsync(computer);

            bool removed = await service.RemoveAsync(computer);
            var all = await service.ReadAllAsync();

            Assert.True(removed);
            Assert.DoesNotContain(computer, all);
        }
    }
}