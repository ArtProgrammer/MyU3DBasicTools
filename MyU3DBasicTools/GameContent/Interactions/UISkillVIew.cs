using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using GameContent.Skill;
using SimpleAI.Logger;
using GameContent.UsableItem;

namespace GameContent.Interaction
{
    public class UISkillVIew : MonoBehaviour
    {
        UISkillInteractor SkillIter = null;

        RectTransform Root = null;

        List<Image> BtnList = new List<Image>();

        // Start is called before the first frame update
        void Start()
        {
            SkillIter = GetComponent<UISkillInteractor>();

            Root = GetComponent<RectTransform>();

            if (!System.Object.ReferenceEquals(null, Root))
            {
                for (int i = 0; i < Root.childCount; i++)
                {
                    Image btnimg = Root.GetChild(i).GetComponent<Image>();
                    Button btn = btnimg.GetComponent<Button>();
                    //btn.onClick.AddListener( () => OnSkilLClick(i));
                    BtnList.Add(btnimg);
                }
            }
        }

        public void OnSkilLClick(int index)
        {
            //SkillIter.TryItem(index);
        }

        public void LoadContent()
        {

        }

        public void OnContentChange()
        {
        }

        private Sprite LoadSprite(string path,
            float pixelsPerUnit = 100.0f)
        {
            //Sprite newsp = new Sprite();
            Texture2D sptx = LoadTex(path);
            if (!System.Object.ReferenceEquals(sptx, null))
            {
                Sprite newsp = Sprite.Create(sptx, 
                    new Rect(0, 0, 
                    sptx.width, sptx.height),
                    new Vector2(0, 0),
                    pixelsPerUnit);

                return newsp;
            }
            else
            {
                TinyLogger.Instance.DebugLog("$failed to load tex: " + path);
            }

            return null;
        }

        private Texture2D LoadTex(string path)
        {
            Texture2D tex;
            byte[] fileData;
            
            if (File.Exists(path))
            {
                //TinyLogger.Instance.ErrorLog("$file: " + path + " exits");
                fileData = File.ReadAllBytes(path);
                tex = new Texture2D(2, 2);

                if (fileData.Length == 0)
                {
                    TinyLogger.Instance.ErrorLog("$ load no bytes data");
                }

                if (tex.LoadImage(fileData))
                {
                    //TinyLogger.Instance.ErrorLog("$ yet load image");
                    return tex;
                }
                else
                {
                    TinyLogger.Instance.ErrorLog("$ failed to load image");
                }
            }
            else
            {
                TinyLogger.Instance.ErrorLog("$file: " + path + " not exits");
            }

            return null;
        }

        public void OnAddItem(int index, BaseUsableData data)
        {
            if (!System.Object.ReferenceEquals(null, data))
            {
                if (!System.Object.ReferenceEquals(null, data.Icon))
                {
                    // change the element with given texture.
                    if (index < BtnList.Count)
                    {
                        Sprite sp = LoadSprite(data.Icon);

                        if (!System.Object.ReferenceEquals(null, sp))
                            BtnList[index].sprite = sp;
                        else
                            TinyLogger.Instance.DebugLog("$ failed to load sprite!");
                            
                    }
                }
            }
        }

        public void OnRemoveItem(int index)
        {
            //
        }

        public void OnItemChange(int index)
        { 

        }
    }
}