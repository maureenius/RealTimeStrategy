using System;
using UnityEngine;

namespace Database
{
    public class ImageDatabase : MonoBehaviour
    {
        public Sprite[] SlotBackgroundImages { get; private set; }

        public Sprite[] RaceFaceImages { get; private set; }
        
        public Sprite FindSlotBackground(string typeName)
        {
            string filename;
            switch (typeName)
            {
                case "無職":
                    filename = "Unemployed";
                    break;
                case "小麦農場":
                    filename = "Farm";
                    break;
                case "さとうきび畑":
                    filename = "Farm";
                    break;
                case "菓子工房":
                    filename = "Farm";
                    break;
                default:
                    throw new InvalidOperationException(typeName);
            }
            
            return Array.Find(SlotBackgroundImages, sprite => sprite.name.Equals(filename));
        }

        public Sprite FindRaceFaceImage(string typename)
        {
            string filename;
            switch (typename)
            {
                case "人間":
                    filename = "Human";
                    break;
                default:
                    throw new InvalidOperationException(typename);
            }

            return Array.Find(RaceFaceImages, sprite => sprite.name.Equals(filename));
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