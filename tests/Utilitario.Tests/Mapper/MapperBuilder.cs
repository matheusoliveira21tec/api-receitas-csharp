using AutoMapper;
using MeuLivroDeReceitas.Application.Services.AutoMapper;

namespace Utilitario.Tests.Mapper;

public class MapperBuilder
{
    public static IMapper Instancia()
    {

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperConfiguration());
        });
        return mockMapper.CreateMapper();
    }
}