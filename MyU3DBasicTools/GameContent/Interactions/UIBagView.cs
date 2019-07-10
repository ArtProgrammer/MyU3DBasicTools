using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleAI.Utils;
using SimpleAI.Logger;
using GameContent.Item;
using GameContent.UsableItem;


namespace GameContent.Interaction
{
    public class UIBagView : MonoBehaviour
    {
        private UIBagInteractor Inter = null;

        public RectTransform Root = null;

        List<Image> BtnList = new List<Image>();

        List<Text> Texts = new List<Text>();

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            BtnList.Clear();
            Texts.Clear();
        }

        public void Initialize()
        {
            Inter = GetComponent<UIBagInteractor>();

            if (!System.Object.ReferenceEquals(null, Root))
            {
                Transform sub = null;
                for (int i = 0; i < Root.childCount; i++)
                {
                    sub = Root.GetChild(i);
                    Image btnimg = sub.GetComponent<Image>();
                    Button btn = sub.GetComponent<Button>();
                    //btn.onClick.AddListener( () => OnSkilLClick(i));
                    BtnList.Add(btnimg);

                    Text txt = sub.GetComponentInChildren<Text>();
                    Texts.Add(txt);
                }

                sub = null;
            }

            //MineResource.Instance.LoadAssetBundleDependencies(Application.dataPath + "/AssetBundles/AssetBundles",
            //     "skillicons");
        }

        public void OnAddItem(BagItem item)
        {
            //OnAddItem(bagitem.Index, bagitem.Data);
            var index = item.Index;
            var data = item.Data;

            TinyLogger.Instance.DebugLog("$ Bag add Item start");
            if (!System.Object.ReferenceEquals(null, data))
            {
                //if (!System.Object.ReferenceEquals(null, data.Icon))
                {
                    // change the element with given texture.
                    if (index < BtnList.Count)
                    {
                        //Sprite sp = MineResource.Instance.LoadSprite(data.Icon);

                        //Sprite sp = MineResource.Instance.LoadSpriteFromAB(Application.dataPath + "/AssetBundles/skillicons",
                        //    data.Icon);

                        TinyLogger.Instance.DebugLog("$ bag add +1");
                        IconData icd = IconManager.Instance.GetIconData(data.IconID);
                        if (!System.Object.ReferenceEquals(icd, null))
                        {
                            TinyLogger.Instance.DebugLog("$ bag sprite ");

                            Sprite sp = MineResource.Instance.LoadSpriteFromAB(icd.Path,
                                icd.Name);
                            TinyLogger.Instance.DebugLog("$ bag sprite loaded");
                            if (!System.Object.ReferenceEquals(null, sp))
                            {
                                BtnList[index].sprite = sp;
                                if (item.Count <= 1)
                                {
                                    Texts[index].enabled = false;
                                }
                                else
                                {
                                    Texts[index].enabled = true;
                                    Texts[index].text = item.Count.ToString();
                                }
                                
                                TinyLogger.Instance.DebugLog("$ bag sprite assign");
                            }
                            else
                                TinyLogger.Instance.DebugLog("$ failed to load sprite!");
                        }

                    }
                }
            }
        }

        //public void OnAddItem(int index, BaseUsableData data)
        //{
        //    TinyLogger.Instance.DebugLog("$ Bag add Item start");
        //    if (!System.Object.ReferenceEquals(null, data))
        //    {
        //        //if (!System.Object.ReferenceEquals(null, data.Icon))
        //        {
        //            // change the element with given texture.
        //            if (index < BtnList.Count)
        //            {
        //                //Sprite sp = MineResource.Instance.LoadSprite(data.Icon);

        //                //Sprite sp = MineResource.Instance.LoadSpriteFromAB(Application.dataPath + "/AssetBundles/skillicons",
        //                //    data.Icon);

        //                TinyLogger.Instance.DebugLog("$ bag add +1");
        //                IconData icd = IconManager.Instance.GetIconData(data.IconID);
        //                if (!System.Object.ReferenceEquals(icd, null))
        //                {
        //                    TinyLogger.Instance.DebugLog("$ bag sprite ");

        //                    Sprite sp = MineResource.Instance.LoadSpriteFromAB(icd.Path,
        //                        icd.Name);
        //                    TinyLogger.Instance.DebugLog("$ bag sprite loaded");
        //                    if (!System.Object.ReferenceEquals(null, sp))
        //                    {
        //                        BtnList[index].sprite = sp;
        //                        TinyLogger.Instance.DebugLog("$ bag sprite assign");
        //                    }
        //                    else
        //                        TinyLogger.Instance.DebugLog("$ failed to load sprite!");
        //                }

        //            }
        //        }
        //    }
        //}

        public void OnRemoveItem(int index)
        {
            //
        }

        public void OnItemChange(int index)
        {

        }
    }
}