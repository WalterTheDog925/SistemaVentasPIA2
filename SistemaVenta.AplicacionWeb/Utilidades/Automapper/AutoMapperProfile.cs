using System.Globalization;
using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.Entity;


namespace SistemaVenta.AplicacionWeb.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion Rol

            #region Usuario
            CreateMap<Usuario, VMUsuario>()
                .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(o => o.EsActivo == true ? 1 : 0))
                .ForMember(destino => destino.NombreRol, opt => opt.MapFrom(o => o.IdRolNavigation.Descripcion));

            CreateMap<VMUsuario, Usuario>()
                .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false))
                .ForMember(destino => destino.IdRolNavigation, opt => opt.Ignore());
            #endregion Usuario  

            #region Negocio
            CreateMap<Negocio, VMNegocio>()
                .ForMember(destino => destino.PorcentajeImpuesto, opt => opt.MapFrom(o => Convert.ToString(o.PorcentajeImpuesto.Value, new CultureInfo("es-MX"))));

            CreateMap<VMNegocio, Negocio>()
                .ForMember(destino => destino.PorcentajeImpuesto, opt => opt.MapFrom(o => Convert.ToDecimal(o.PorcentajeImpuesto, new CultureInfo("es-MX"))));
            #endregion Negocio

            #region Categoria
            CreateMap<Categoria, VMCategoria>()
                .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(o => o.EsActivo == true ? 1 : 0));

            CreateMap<VMCategoria, Categoria>()
                .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false));
            #endregion Categoria

            #region Producto
            CreateMap<Producto, VMProducto>()
                .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(o => o.EsActivo == true ? 1 : 0))
                .ForMember(destino => destino.NombreCategoria, opt => opt.MapFrom(o => o.IdCategoriaNavigation.Descripcion))
                .ForMember(destino => destino.Precio, opt => opt.MapFrom(o => Convert.ToString(o.Precio.Value, new CultureInfo("es-MX"))));

            CreateMap<VMProducto, Producto>()
                .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false))
                .ForMember(destino => destino.IdCategoriaNavigation, opt => opt.Ignore())
                .ForMember(destino => destino.Precio, opt => opt.MapFrom(o => Convert.ToDecimal(o.Precio, new CultureInfo("es-MX"))));
            #endregion Producto

            #region TipoDocumentoVenta
            CreateMap<TipoDocumentoVenta, VMTipoDocumentoVenta>().ReverseMap();
            #endregion TipoDocumentoVenta

            #region Venta
            CreateMap<Venta, VMVenta>()
                .ForMember(destino => destino.TipoDocumentoVenta, opt => opt.MapFrom(o => o.IdTipoDocumentoVentaNavigation.Descripcion))
                .ForMember(destino => destino.Usuario, opt => opt.MapFrom(o => o.IdUsuarioNavigation.Nombre))
                .ForMember(destino => destino.SubTotal, opt => opt.MapFrom(o => Convert.ToString(o.SubTotal.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.ImpuestoTotal, opt => opt.MapFrom(o => Convert.ToString(o.ImpuestoTotal.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.Total, opt => opt.MapFrom(o => Convert.ToString(o.Total.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.FechaRegistro, opt => opt.MapFrom(o => o.FechaRegistro.Value.ToString("dd/MM/yyyy")));

            CreateMap<VMVenta, Venta>()
                .ForMember(destino => destino.SubTotal, opt => opt.MapFrom(o => Convert.ToString(o.SubTotal, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.ImpuestoTotal, opt => opt.MapFrom(o => Convert.ToString(o.ImpuestoTotal, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.Total, opt => opt.MapFrom(o => Convert.ToString(o.Total, new CultureInfo("es-MX"))));
            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, VMDetalleVenta>()
                .ForMember(destino => destino.Precio, opt => opt.MapFrom(o => Convert.ToString(o.Precio.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.Total, opt => opt.MapFrom(o => Convert.ToString(o.Total.Value, new CultureInfo("es-MX"))));

            CreateMap<VMDetalleVenta, DetalleVenta>()
                .ForMember(destino => destino.Precio, opt => opt.MapFrom(o => Convert.ToDecimal(o.Precio, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.Total, opt => opt.MapFrom(o => Convert.ToDecimal(o.Total, new CultureInfo("es-MX"))));

            CreateMap<DetalleVenta, VMReporteVenta>()
                .ForMember(destino => destino.FechaRegistro, opt => opt.MapFrom(o => o.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))
                .ForMember(destino => destino.NumeroVenta, opt => opt.MapFrom(o => o.IdVentaNavigation.NumeroVenta))
                .ForMember(destino => destino.TipoDocumento, opt => opt.MapFrom(o => o.IdVentaNavigation.IdTipoDocumentoVentaNavigation.Descripcion))
                .ForMember(destino => destino.DocumentoCliente, opt => opt.MapFrom(o => o.IdVentaNavigation.DocumentoCliente))
                .ForMember(destino => destino.NombreCliente, opt => opt.MapFrom(o => o.IdVentaNavigation.NombreCliente))
                .ForMember(destino => destino.SubTotalVenta, opt => opt.MapFrom(o => Convert.ToString(o.IdVentaNavigation.SubTotal.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.ImpuestoTotalVenta, opt => opt.MapFrom(o => Convert.ToString(o.IdVentaNavigation.ImpuestoTotal.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.TotalVenta, opt => opt.MapFrom(o => Convert.ToString(o.IdVentaNavigation.Total.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.Producto, opt => opt.MapFrom(o => o.DescripcionProducto))
                .ForMember(destino => destino.Precio, opt => opt.MapFrom(o => Convert.ToString(o.Precio.Value, new CultureInfo("es-MX"))))
                .ForMember(destino => destino.Total, opt => opt.MapFrom(o => Convert.ToString(o.Total.Value, new CultureInfo("es-MX"))));
            #endregion DetalleVenta

            #region Menu
            CreateMap<Menu, VMMenu>()
                .ForMember(destino => destino.SubMenus, opt => opt.MapFrom(o => o.InverseIdMenuPadreNavigation));
            #endregion Menu
        }
    }
}
