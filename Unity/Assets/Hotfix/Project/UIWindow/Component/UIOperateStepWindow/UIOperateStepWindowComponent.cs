using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ETModel;
using DG.Tweening;
using System.Linq;

namespace ETHotfix
{
    public partial class UIOperateStepWindowComponent : UIWindowComponent
    {
        public static bool IsStepOperateWindowOpen;
         private void RegisterEvent()
        {
            LastStep.GetComponent<Button>().onClick.AddListener(()=>
            {
                 var index = StartMissionEvent.CurrentStepIndex-1;
                if (index < 0)
                {
                    index = 0;
                }
                Game.EventSystem.Run(EventIdType.StartMissionEvent, index);
              });

            NextStep.GetComponent<Button>().onClick.AddListener(() =>
            {
                var index = StartMissionEvent.CurrentStepIndex + 1;
                var count = Game.Scene.GetComponent<ProjectConfigComponent>().TryGetAllMission().Count;
                if (index >count)
                {
                    index = count;
                }
                Game.EventSystem.Run(EventIdType.StartMissionEvent, index);
            });
            Reset.GetComponent<Button>().onClick.AddListener(() =>
            {
                Game.EventSystem.Run(EventIdType.CloseHightlightEvent);
                Game.EventSystem.Run(EventIdType.StartMissionEvent, 0);
            });
        }

        protected override void Show()
        {
            IsStepOperateWindowOpen = true;
            Game.EventSystem.Run(EventIdType.CloseHightlightEvent);
            Game.EventSystem.Run(EventIdType.StartMissionEvent, 0);
            base.Show();
        }
        protected override void Hide()
        {
            IsStepOperateWindowOpen = false;
            base.Hide();
        }
    }
}

