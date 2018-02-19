using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scraps_HUD_Inventory : MonoBehaviour {

    private static Scraps_HUD_Inventory _instance;
    public static Scraps_HUD_Inventory instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Scraps_HUD_Inventory>();
            return _instance;
        }
    }

    public Color itemColor;
    public Color allItemColor;

    // public bool autoScanPickups = false;
    public KeyCode toggleInputKey = KeyCode.I;
    public Vector2 ScreenPosition = new Vector2(0.1f, 0.1f);
    public bool AbsolutePos = false;
    public string HeaderTitle;
    public Texture2D HeaderTexture;
    public Texture2D EntryDashesTexture;
    public Texture2D EntryFrameTexture;
    public Texture2D EntryShevronTexture;
    public Texture2D TailTexture;
    public Texture2D UnknownTexture;
    public string UnknownLabel;

    private Vector2 headPadding = new Vector2(0, 0);
    private Vector2 itemPadding = new Vector2(0, 2);
    private Vector2 tailPadding = new Vector2(0, 2);

    private GUIStyle labelStyle = new GUIStyle();

    private bool isHidden = false;

    struct PickupInfo
    {
        public string type;
        public Texture2D icon;
        public int count;
        public int countMax;
        public bool isUnknown;
    }

    List<PickupInfo> pickupTypes = new List<PickupInfo>();

	// Use this for initialization
	void Start () {

        // NOTE: Now called from LevelStreaming.cs
        //if (autoScanPickups)
            //ScanWorldForPickupTypes();
	}

    void OnGUI()
    {
        if (!isHidden)
        {
            // A percentage across the screen or an absolute position
            Vector2 startPos = (AbsolutePos ? ScreenPosition : new Vector2(Screen.width * ScreenPosition.x, Screen.height * ScreenPosition.y));

            // HEADER
            DrawHeader(ref startPos, headPadding);

            // ITEMS
            for (int i = 0; i < pickupTypes.Count; ++i)
            {
                // Last item detection for drawing the tail (due to art)
                bool isLast = ((i + 1) == pickupTypes.Count);

                // TAIL (Draw tail here due to how the art was created)
                if (isLast)
                    DrawTail(startPos, tailPadding);

                DrawEntry(pickupTypes[i], ref startPos, itemPadding, isLast);
            }

            // TAIL
            //// DrawTail(startPos, tailPadding);
        }
    }

    private void DrawHeader(ref Vector2 _startPos, Vector2 _offset)
    {
        // Image (backdrop)
        /*GUI.DrawTexture(new Rect(_startPos.x, _startPos.y, HeaderTexture.width, HeaderTexture.height), HeaderTexture);
        
        // Label (text)
        {
            Rect labelRect = new Rect(_startPos.x + 10, _startPos.y +1.5f, HeaderTexture.width, HeaderTexture.height);
            labelStyle.alignment = TextAnchor.UpperLeft;
            labelStyle.normal.textColor = Color.black;
            labelStyle.fontStyle = FontStyle.Bold;
            GUI.TextArea(labelRect, HeaderTitle, labelStyle);
        }*/

        // Step (position for next item)
        _startPos.y += HeaderTexture.height;
        _startPos += _offset;
    }

    private void DrawEntry(PickupInfo _pickup, ref Vector2 _startPos, Vector2 _offset, bool isLast)
    {
        // Apply Offset
        _startPos += _offset;

        // Is maxed out?
        Color colorTexture = (_pickup.count < _pickup.countMax ? itemColor : allItemColor);
        Color colorPrev = GUI.color;

        // Back drop frame (dashed lines)
        // Don't draw frame if the last item. The tail contains its own frame in the art
        if (!isLast)
            GUI.DrawTexture(new Rect(_startPos.x, _startPos.y, EntryDashesTexture.width, EntryDashesTexture.height), EntryDashesTexture);

        // Icon BD border
        GUI.color = colorTexture;
        Vector2 pos = _startPos;
        pos.x += (EntryDashesTexture.width * 0.2f) - EntryFrameTexture.width / 2.0f; //0.25f
        pos.y += EntryDashesTexture.height / 2 - EntryFrameTexture.height / 2;
        GUI.DrawTexture(new Rect(pos.x, pos.y, EntryFrameTexture.width, EntryFrameTexture.height), EntryFrameTexture);

        /*// ICON
        Texture2D icon = (_pickup.isUnknown ? UnknownTexture : _pickup.icon);
        */GUI.color = colorPrev;
        /*pos.x += EntryFrameTexture.width / 2 - icon.width / 2;
        pos.y += EntryFrameTexture.height / 2 - icon.height / 2;
        GUI.DrawTexture(new Rect(pos.x, pos.y, icon.width, icon.height), icon);*/

        // SHEVRON
        //GUI.color = colorTexture;
        pos = _startPos;
        pos.x += (EntryDashesTexture.width * 0.62f) - (EntryShevronTexture.width / 2);  //*0.7f
        pos.y += EntryDashesTexture.height / 2 - EntryShevronTexture.height / 2;
        GUI.DrawTexture(new Rect(pos.x, pos.y, EntryShevronTexture.width * 1.2f, EntryShevronTexture.height), EntryShevronTexture);

        // ITEM LABEL (Name + Counts)
        {
            string itemLabel = (_pickup.isUnknown ? UnknownLabel : _pickup.type);
            itemLabel += "\n" + _pickup.count + " / " + _pickup.countMax;

            Rect labelRect = new Rect(pos.x * 1.1f, pos.y, EntryShevronTexture.width, EntryShevronTexture.height);
            labelStyle.alignment = TextAnchor.MiddleCenter;
            labelStyle.normal.textColor = Color.white;
            
            GUI.TextArea(labelRect, itemLabel, labelStyle);
        }

        // STEP
        _startPos.y += EntryDashesTexture.height;

        // Restore old GUI color
        //GUI.color = colorPrev;
    }

    private void DrawTail(Vector2 _startPos, Vector2 _offset)
    {
        _startPos += _offset;

        GUI.DrawTexture(new Rect(_startPos.x, _startPos.y, TailTexture.width, TailTexture.height), TailTexture);
    }

    //get amount of a type of pickup
    public int GetPickupAmount(string typeName)
    {
        return pickupTypes.Find(x => x.type.Contains(typeName)).count;
    }

    //remove all pickups from inventory of type
    public void RemovePickup(string typeName, int amount)
    {
        int idx = pickupTypes.IndexOf(pickupTypes.Find(x => x.type.Contains(typeName)));

        PickupInfo curPickup = pickupTypes[idx];

        if (curPickup.count > 0)
        {
            curPickup.count -= amount;
        }
        else
        {
            curPickup.count = 0;
        }

        pickupTypes[idx] = curPickup;
    }

    //remove all pickups from inventory of type
    public void RemoveAllPickups(string typeName)
    {
        pickupTypes.Remove(pickupTypes.Find(x => x.type == typeName));
    }

    private void AddPickup(Scraps_HUD_Pickup _pickup)
    {
        PickupInfo newPickup = new PickupInfo();
        newPickup.type = _pickup.typeName;
        newPickup.icon = _pickup.icon;
        newPickup.isUnknown = _pickup.isUnknown;

        pickupTypes.Add(newPickup);
    }

    public void AccountForPickup(Scraps_HUD_Pickup _pickup)
    {
        int idx = GetItemIndex(_pickup);
        if (idx < 0)
        {
            AddPickup(_pickup);
            idx = pickupTypes.Count - 1;
        }

        PickupInfo curPickup = pickupTypes[idx];
        curPickup.countMax += 1;
        pickupTypes[idx] = curPickup;
    }

    public void CountPickup(Scraps_HUD_Pickup _pickup)
    {
        bool bFound = false;
        for (int i = 0; i < pickupTypes.Count; ++i )
        {
            PickupInfo curPickup = pickupTypes[i];
            if (curPickup.type == _pickup.typeName)
            {
                bFound = true;
                curPickup.count += 1;
                curPickup.isUnknown = false;
                pickupTypes[i] = curPickup;
            }
        }

        if (!bFound)
            print("Pick \"" + _pickup.typeName  + "\" not found.");
    }

    public void ScanWorldForPickupTypes()
    {
        pickupTypes.RemoveRange(0, pickupTypes.Count);
        Scraps_HUD_Pickup[] pickups = FindObjectsOfType<Scraps_HUD_Pickup>();
        for (int i = 0; i < pickups.Length; ++i)
        {
            int idx = GetItemIndex(pickups[i]);
            if (idx < 0)
            {
                AddPickup(pickups[i]);
                idx = pickupTypes.Count-1;
            }

            PickupInfo curPickup = pickupTypes[idx];
            curPickup.countMax += 1;
            pickupTypes[idx] = curPickup;
        }
    }

    private int GetItemIndex(Scraps_HUD_Pickup _pickup)
    {
        for (int i = 0; i < pickupTypes.Count; ++i)
        {
            PickupInfo curPickup = pickupTypes[i];
            if (curPickup.type == _pickup.typeName)
            {
                return i;
            }
        }
        return -1;
    }
}
