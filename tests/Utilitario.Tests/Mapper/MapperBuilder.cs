using AutoMapper;
using MeuLivroDeReceitas.Application.Services.AutoMapper;
using Utilitario.Tests.Hashids;

namespace Utilitario.Tests.Mapper;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var hashids = HashidsBuilder.Instance().Build();

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperConfiguration(hashids));
        });
        return mockMapper.CreateMapper();
    }
}