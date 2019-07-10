using System.Collections.Generic;
using System.Xml;

class OsmWay : BaseOsm
{
    //  <way id="10427012" visible="true" version="11" changeset="29929531" timestamp="2015-04-02T13:57:42Z" user="joakimfors" uid="306096">

    public ulong ID { get; private set; }
    public bool Visible { get; private set; }
    public List<ulong> NodeIDs { get; private set; }
    public bool IsBoundary { get; private set; }

    public bool IsBuilding { get; private set; }

    public bool IsRoad { get; private set; }

    public float Height { get; private set; }

    public string Name { get; private set; }
    public int Lanes { get; private set; }

    public OsmWay(XmlNode node)
    {
        NodeIDs = new List<ulong>();
        Height = 3.0f;
        Lanes = 1;
        Name = "";

        ID = GetAttribute<ulong>("id", node.Attributes);
        Visible = GetAttribute<bool>("visible", node.Attributes);

        XmlNodeList nds = node.SelectNodes("nd");
        foreach (XmlNode n in nds)
        {
            ulong refNo = GetAttribute<ulong>("ref", n.Attributes);
            NodeIDs.Add(refNo);
        }
        // TODO: Determine what type of way it is: is it a road / boundary etc

        if (NodeIDs.Count > 1)
        {
            IsBoundary = NodeIDs[0] == NodeIDs[NodeIDs.Count - 1];
        }

        // Read the tags
        XmlNodeList tags = node.SelectNodes("tag");
        foreach (XmlNode t in tags)
        {
            string key = GetAttribute<string>("k", t.Attributes);
            if (key == "building:levels")
            {
                Height = 3.0f * GetAttribute<float>("v", t.Attributes);
            }
            else if (key == "height")
            {
                Height = 0.3048f * GetAttribute<float>("v", t.Attributes);
            }
            else if (key == "building")
            {
                IsBuilding = true; // GetAttribute<string>("v", t.Attributes) == "yes";
            }
            else if (key == "highway")
            {
                IsRoad = true;
            }
            else if (key == "lanes")
            {
                Lanes = GetAttribute<int>("v", t.Attributes);
            }
            else if (key == "name")
            {
                Name = GetAttribute<string>("v", t.Attributes);
            }
        }
    }
}
