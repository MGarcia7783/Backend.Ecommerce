using AutoMapper;
using Ecommerce.Application.Dtos.Categoria;
using Ecommerce.Application.Dtos.Pago;
using Ecommerce.Application.Dtos.Pedido;
using Ecommerce.Application.Dtos.Producto;
using Ecommerce.Application.Dtos.Usuario;
using Ecommerce.Application.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Api.Mapping
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            #region Mapeo del modelo categoria

            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<CrearCategoriaDTO, Categoria>();
            CreateMap<ActualizarCategoriaDTO, Categoria>()
                .ForMember(dest => dest.estado, opt => opt.Ignore())
                .ForMember(dest => dest.fechaRegistro, opt => opt.Ignore());

            #endregion

            #region Mapeo del modelo producto

            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.nombreCategoria,
                            opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.nombreCategoria : string.Empty));

            CreateMap<CrearProductoDTO, Producto>();
            CreateMap<ActualizarProductoDTO, Producto>()
                .ForMember(dest => dest.Categoria, opt => opt.Ignore())
                .ForMember(dest => dest.estado, opt => opt.Ignore())
                .ForMember(dest => dest.fechaRegistro, opt => opt.Ignore());

            #endregion

            #region Mapeo del modelo usuario

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Rol, opt => opt.Ignore());
            CreateMap<CrearUsuarioDTO, Usuario>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.nombreCompeto, opt => opt.MapFrom(src => src.nombreCompleto));

            CreateMap<Usuario, LoginRespuestaUsuarioDTO>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src));

            #endregion

            #region Mapeo del modelo pedidos

            CreateMap<Pedido, PedidoDTO>();
            CreateMap<DetallePedido, DetallePedidoDTO>();

            CreateMap<CrearPedidoDTO, Pedido>();
            CreateMap<CrearDetallePedidoDTO, DetallePedido>();

            #endregion

            #region Mapeo del modelo pagos

            CreateMap<Pago, PagoDTO>()
                .ForMember(dest => dest.totalPedido,
                            opt => opt.MapFrom(src => src.Pedido != null ? src.Pedido.total : (decimal?)null))
                .ForMember(dest => dest.idUsuario,
                            opt => opt.MapFrom(src => src.Pedido != null ? src.Pedido.idUsuario : null))
                .ForMember(dest => dest.nombreCompleto,
                            opt => opt.MapFrom(src => src.Pedido != null ? src.Pedido.Usuario!.nombreCompeto : null));

            CreateMap<CrearPagoDTO, Pago>();

            #endregion
        }
    }
}
