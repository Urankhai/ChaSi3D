using System.Xml;
using UnityEngine;

class OsmBounds : BaseOsm
{
    public float MinLat { get; private set; }
    public float MaxLat { get; private set; }
    public float MinLon { get; private set; }
    public float MaxLon { get; private set; }
    public Vector3 Center { get; private set; }
    
    //minlat="55.7051700" minlon="13.1871800" maxlat="55.7077900" maxlon="13.1939600"
    public OsmBounds(XmlNode node)
    {
        // Get the values from the node
        MinLat = GetAttribute<float>("minlat", node.Attributes);
        MaxLat = GetAttribute<float>("maxlat", node.Attributes);
        MinLon = GetAttribute<float>("minlon", node.Attributes);
        MaxLon = GetAttribute<float>("maxlon", node.Attributes);
        
        // Create the centre location
        float x = (float)((MercatorProjection.lonToX(MaxLon) + MercatorProjection.lonToX(MinLon)) / 2);
        float y = (float)((MercatorProjection.latToY(MaxLat) + MercatorProjection.latToY(MinLat)) / 2);
        Center = new Vector3(x, 0, y);

    }
        
}
