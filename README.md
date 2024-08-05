    public interface IRepository<T>
    {
        public Task<IQueryable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task AddAsync(Entity entity);
        public Task UpdateAsync(Entity entity);
        public Task Delete(int id);
    }


        public class OracleEntityRepository : IRepository<Entity>
    {
        private readonly OracleDbContext _context;
        public OracleEntityRepository(OracleDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<Entity>> GetAllAsync() { }
        public async Task<Entity> GetByIdAsync(int id) { }
        public async Task AddAsync(Entity entity) { }
        public async Task UpdateAsync(Entity entity) { }
        public async Task Delete(int id) { }
    }

        public class PostgreEntityRepository : IRepository<Entity>
    { 
        private readonly PostgreDbContext _context;
        public PostgreEntityRepository(PostgreDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Entity>> GetAllAsync() { }
        public async Task<Entity> GetByIdAsync(int id) { }
        public async Task AddAsync(Entity entity) { }
        public async Task UpdateAsync(Entity entity) { }
        public async Task Delete(int id) { }
    }

    Есть у меня условно интерфейс с обобщением, от которого наследуют классы репозитории, со своими сущностями. 
    Как мне теперь написать верно фабрику, условно если я знаю какой тип репозитория мне нужно создавать, я пытался так, но чет не догоняю

        public interface IRepositoryFactory<T>
    {
        public Task<IRepository<T>> CreateRepository();
    }    public class RepositoryFactory<T> : IRepositoryFactory<T>
    {
        public async Task<IRepository<T>> CreateRepository() 
        {
            IRepository<T> repository = new 
        }

    }

    сделать так, типа я хотел типа указать, возвращает irepository<T> бля короче хуету делаю, помоги 
