using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for Maps
/// </summary>
public class Maps
{
    public Maps()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string Teste()
    {
        return @"var map;
      function initMap() {
        map = new google.maps.Map(document.getElementById('mapa'), {
          center: {lat: -34.397, lng: 150.644},
          zoom: 8
        });
      }";
    }

    public static string Teste2()
    {
        return @"function InicializaMapa() {
                var latlng = new google.maps.LatLng(-15.682543, -47.978874);
                var opcoes = {
                    zoom: 8,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                var mapa = new google.maps.Map(document.getElementById('mapa'), opcoes);
            }
            window.onload = InicializaMapa;";
    }

    public static string Teste3()
    {
        return @"function initMap() {
    var myLatLng = {lat: -5.7637635, lng: -42.3078464};
    

      var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 5,
        center: myLatLng
      });
        
        var capgv ={lat: -3.799454, lng: -38.524969};
        var marker = new google.maps.Marker({
        position: capgv,
        map: map,
        title: 'CAPGV (Centro Administrativo)'
  });
}";
    }
    public static string ConstruirMapa()
    {
        return @"function initMap() {
var myLatLng = {lat: -5.7637635, lng: -42.3078464};
    

var map = new google.maps.Map(document.getElementById('map'), {
zoom: 5,
center: myLatLng
});
"
            //+ Markers.returnMarkers(retornaListaTeste()) +
            + Markers.returnMarkers(retornaMarkers(Markers.tpIcon.printer_on)) +
"}";
    }

    public static List<Markers> retornaListaTeste()
    {
        List<Markers> lista = new List<Markers>();

        Markers m1 = new Markers("CAPGV", "-3.8089611", "-38.5357721",Markers.tpIcon.tecnico);
        Markers m2 = new Markers("AG_Montese", "-3.8030445", "-38.5585481", Markers.tpIcon.printer_off);
        Markers m3 = new Markers("AG_Centro", "-3.7372224", "-38.5249448", Markers.tpIcon.printer_off);
        lista.Add(m1);
        lista.Add(m2);
        lista.Add(m3);
        return lista;
    }

    public static List<Markers> retornaMarkers(Markers.tpIcon tipoIcone)
    {
        List<Markers> lista = new List<Markers>();

        DataTable dtMarkers = DAO.retornaDt("select descricao, nomeSimples, lat, lng from dbo.markers");
        foreach (DataRow marker in dtMarkers.Rows)
        {
            lista.Add(new Markers(marker["nomeSimples"].ToString(),marker["lat"].ToString(),marker["lng"].ToString(), tipoIcone));
        }
        return lista;
    }
}