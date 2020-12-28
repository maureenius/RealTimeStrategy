using System;
using UnityEngine;

#nullable enable

namespace Store
{
    public class ImageStore : MonoBehaviour
    {
       private Sprite[]? SlotBackgroundImages { get; set; }

        private Sprite[]? RaceFaceImages { get; set; }
        
        private Sprite[]? BuildingImages { get; set; }
        
        public Sprite FindSlotBackground(string typeName)
        {
            if (SlotBackgroundImages == null) throw new NullReferenceException();
            
            string filename = typeName switch
            {
                "無職" => "Unemployed",
                "小麦農場" => "Farm",
                "さとうきび畑" => "Farm",
                "菓子工房" => "Farm",
                _ => throw new InvalidOperationException(typeName)
            };

            return Array.Find(SlotBackgroundImages, sprite => sprite.name.Equals(filename));
        }

        public Sprite FindNoneBackground()
        {
            if (SlotBackgroundImages == null) throw new NullReferenceException();
            
            return Array.Find(SlotBackgroundImages, sprite => sprite.name.Equals("None"));
        }
        
        public Sprite FindRaceFaceImage(string typename)
        {
            if (RaceFaceImages == null) throw new NullReferenceException();

            return Array.Find(RaceFaceImages, sprite => sprite.name.Equals(typename));
        }

        public Sprite FindNoneRaceFaceImage()
        {
            if (RaceFaceImages == null) throw new NullReferenceException();
            
            return Array.Find(RaceFaceImages, sprite => sprite.name.Equals("None"));
        }

        public Sprite FindBuildingImage(string typeName)
        {
            if (BuildingImages == null) throw new NullReferenceException();

            string filename = typeName switch
            {
                "小麦農場" => "farm",
                "さとうきび畑" => "farm",
                "菓子工房" => "farm",
                _ => throw new InvalidOperationException(typeName)
            };

            return Array.Find(BuildingImages, sprite => sprite.name.Equals(filename));
        }
        
        private void LoadSprites()
        {
            SlotBackgroundImages = Resources.LoadAll<Sprite> ("Images/SlotBackground/");
            RaceFaceImages = Resources.LoadAll<Sprite> ("Images/RaceFace/");
            BuildingImages = Resources.LoadAll<Sprite>("Images/Building/");
        }
        
        private void Start()
        {
            LoadSprites();
        }
    }
}
