using AutoMapper;

namespace ECommerce.API.Helpers
{
    public static class ObjectMapper
    {
        //Lazy ihtiyaç olduğunda çağırmak için
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfig>();
            });

            return config.CreateMapper();   
        });

        public static IMapper Mapper => lazy.Value;//get
    }
}
