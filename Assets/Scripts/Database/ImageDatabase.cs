using System;
using UnityEngine;

#nullable enable

namespace Database
{
    public class ImageDatabase : MonoBehaviour
    {
        private Sprite[]? SlotBackgroundImages { get; set; }

        private Sprite[]? RaceFaceImages { get; set; }
        
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
            
            string filename = typename switch
            {
                "人間" => "Human",
                "エルフ" => "Elf",
                _ => throw new InvalidOperationException(typename)
            };

            return Array.Find(RaceFaceImages, sprite => sprite.name.Equals(filename));
        }

        public Sprite FindNoneRaceFaceImage()
        {
            if (RaceFaceImages == null) throw new NullReferenceException();
            
            return Array.Find(RaceFaceImages, sprite => sprite.name.Equals("None"));
        }
        
        private void LoadSprites()
        {
            SlotBackgroundImages = Resources.LoadAll<Sprite> ("Images/SlotBackground/");
            RaceFaceImages = Resources.LoadAll<Sprite> ("Images/RaceFace/");
        }
        
        private void Start()
        {
            LoadSprites();
        }
    }
}