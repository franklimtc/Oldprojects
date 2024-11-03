using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for Markers
/// </summary>
public class Markers
{
    #region campos
    public enum tpIcon { tecnico, printer_off, printer_on };

    private string _descricao;

    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
    private string _lat;

    public string Lat
    {
        get { return _lat; }
        set { _lat = value; }
    }
    private string _lng;

    public string Lng
    {
        get { return _lng; }
        set { _lng = value; }
    }

    private tpIcon _icone;
    public tpIcon Icone
    {
        get { return _icone; }
        set { _icone = value; }
    }
    #endregion
    public Markers(string nome, string latitude, string longitude)
	{
		//
		// TODO: Add constructor logic here
		//
        this.Descricao = nome;
        this.Lat = latitude;
        this.Lng = longitude;
	}
    public Markers(string nome, string latitude, string longitude, tpIcon icon)
    {
        //
        // TODO: Add constructor logic here
        //
        this.Descricao = nome;
        this.Lat = latitude;
        this.Lng = longitude;
        this.Icone = icon;
    }

    public string returnMarker()
    {

        string script = null;
        if (this.Icone == null)
        {
            script = @"var " + this.Descricao.Replace(" ", "") + @"LatLng ={lat: " + this.Lat + @", lng: " + this.Lng + @"};
            var " + this.Descricao.Replace(" ", "") + @"marker = new google.maps.Marker({
            position: " + this.Descricao.Replace(" ", "") + @"LatLng,
            draggable: true,
            animation: google.maps.Animation.DROP,
            map: map,
            title: '" + this.Descricao
    + @"'});";
        }
        else
        {
            string varImage = null;
            string varImageName = string.Format("{0}image", this.Descricao.Replace(" ", ""));

            switch (this.Icone)
            {
                case tpIcon.printer_off:
                    varImage = string.Format("var {0}image = '{1}';", this.Descricao.Replace(" ", ""), @"img/printer_off.png");
                    break;
                case tpIcon.printer_on:
                    varImage = string.Format("var {0}image = '{1}';", this.Descricao.Replace(" ", ""), @"img/printer_on.jpg");
                    break;
                case tpIcon.tecnico:
                    varImage = string.Format("var {0}image = '{1}';", this.Descricao.Replace(" ", ""), @"img/tecnico.jpg");
                    break;
            }
            script = @"var " + this.Descricao.Replace(" ", "") + @"LatLng ={lat: " + this.Lat + @", lng: " + this.Lng + @"};
            " + varImage + @"
            var " + this.Descricao.Replace(" ", "") + @"marker = new google.maps.Marker({
            position: " + this.Descricao.Replace(" ", "") + @"LatLng,
            draggable: true,
            animation: google.maps.Animation.DROP,
            map: map,
            icon: " + varImageName + @",
            title: '" + this.Descricao
    + @"'});";
        }


        return script;

    }
    public static string returnMarker(Markers marc)
    {
        return @"var " + marc.Descricao + @"LatLng ={lat: " + marc.Lat + @", lng: " + marc.Lng + @"};
            var " + marc.Descricao + @"marker = new google.maps.Marker({
            position: " + marc.Descricao + @"LatLng,
            draggable: true,
            animation: google.maps.Animation.DROP,
            map: map,
            title: '" + marc.Descricao
+ @"'});";

    }
    public static string returnMarkers(List<Markers> lista)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Markers m in lista)
        {
            sb.AppendLine(m.returnMarker());
        }
        return sb.ToString();
    }

//    public string returnMarker()
//    {
//        return @"var capgv ={lat: " + this.Lat + @", lng: " + this.Lng + @"};
//            var marker = new google.maps.Marker({
//            position: " + this.Descricao + @",
//            map: map,
//            title: '" + this.Descricao + @""
//            + @"var contentString" + this.Descricao + @" = <h1>" + this.Descricao + @"</h1>"
//            + @"var infowindow" + this.Descricao + @" = new google.maps.InfoWindow({
//    content: contentString });"
//        + @"marker.addListener('click', function() {
//        infowindow" + this.Descricao + @".open(map, marker);";
//    }

    

}