using System;
using System.Collections.Generic;

namespace TRPGTest
{
    public class QuestManager
    {
        // 퀘스트 클래스
        public class Quest
        {
            // 프로퍼티
            public string Name { get; set; } // 퀘스트 이름
            public string Description { get; set; } // 퀘스트 설명
            public int ID { get; set; } // 퀘스트 ID
            public int Goal { get; set; } // 퀘스트 목표
            public int Progress { get; set; } // 퀘스트 진행 상황
            public bool IsComplete => Progress >= Goal; // 퀘스트 완료 여부
            public bool IsAccepted { get; set; } // 퀘스트 수락 여부

            // 생성자
            public Quest(string name, string description, int id, int goal)
            {
                Name = name;
                Description = description;
                ID = id;
                Goal = goal;
                Progress = 0;
                IsAccepted = false; // 처음에는 수락되지 않은 상태로 초기화
            }
        }

        private List<Quest> quests = new List<Quest>(); // 퀘스트 목록

        // 생성자
        public QuestManager()
        {
            // 퀘스트 초기화
            InitializeQuests();
        }

        // 퀘스트 초기화 메서드
        private void InitializeQuests()
        {
            // 여기에 퀘스트를 추가할 수 있습니다.
            // 예시: quests.Add(new Quest("퀘스트 이름", "퀘스트 설명", 퀘스트 번호, 목표량));
            quests.Add(new Quest("마을을 위협하는 미니언 처치", "미니언을 처치하여 마을을 지키세요.", 1, 5));
            quests.Add(new Quest("장비를 장착해보자", "장비를 장착하여 더 강해지세요.", 2, 1));
            quests.Add(new Quest("더욱 더 강해지기!", "더욱 강력한 장비를 얻어라.", 3, 1));
        }

        // 퀘스트 메뉴 표시
        public void ShowQuests()
        {
            Console.Clear();
            Console.WriteLine("**Quest!!**\n");

            // 수락한 퀘스트와 진행 상황 표시
            Console.WriteLine("수락한 퀘스트 목록:");
            foreach (var quest in quests)
            {
                if (quest.IsAccepted)
                {
                    Console.WriteLine($"{quest.Name}: {quest.Progress}/{quest.Goal}");
                }
            }

            Console.WriteLine("\n전체 퀘스트 목록:");
            // 퀘스트 목록 출력
            for (int i = 0; i < quests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quests[i].Name}");
            }

            Console.WriteLine("\n원하시는 퀘스트를 선택해주세요.");
            string input = Console.ReadLine();

            int questIndex;
            if (int.TryParse(input, out questIndex) && questIndex >= 1 && questIndex <= quests.Count)
            {
                // 선택한 퀘스트의 상세 정보 표시
                ShowQuestDetails(questIndex - 1);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }


        // 선택한 퀘스트의 상세 정보 표시
        private void ShowQuestDetails(int index)
        {
            Console.Clear();
            Console.WriteLine("**Quest!!**\n");
            Console.WriteLine(quests[index].Name + "\n");
            Console.WriteLine(quests[index].Description + "\n");
            Console.WriteLine($"- 목표량: {quests[index].Goal}\n");

            if (quests[index].IsComplete)
            {
                Console.WriteLine("퀘스트를 이미 완료하셨습니다.");
            }
            else
            {
                Console.WriteLine("1. 퀘스트 수락");
            }

            Console.WriteLine("2. 메인 메뉴로 돌아가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // 퀘스트 수락
                    AcceptQuest(index);
                    break;
                case "2":
                    // 메인 메뉴로 돌아가기
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }
        }

        // 퀘스트 수락 메서드
        private void AcceptQuest(int index)
        {
            Console.WriteLine($"퀘스트 '{quests[index].Name}'를 수락하셨습니다.");
            // 여기에 퀘스트 수락 시 추가적으로 해야 할 작업을 추가할 수 있습니다.
            Console.ReadKey();
        }

        // 몬스터가 죽을 때 호출되는 메서드
        public void MonsterDies()
        {
            // 퀘스트 목록 중에서 마을을 위협하는 미니언 처치 퀘스트를 찾아서 진행 상황을 업데이트합니다.
            foreach (var quest in quests)
            {
                if (quest.ID == 1) // 마을을 위협하는 미니언 처치 퀘스트의 ID가 1이라고 가정합니다.
                {
                    quest.Progress++; // 진행 상황을 증가시킵니다.
                    if (quest.IsComplete)
                    {
                        Console.WriteLine($"퀘스트 '{quest.Name}'를 완료하셨습니다!");
                        Console.WriteLine("보상을 받으세요!");
                        // 여기에 퀘스트 보상을 주는 로직을 추가할 수 있습니다.
                        GiveQuestReward(); // 퀘스트 보상 주는 메서드 호출
                    }
                }
            }
        }

        // 퀘스트 보상 주는 메서드
        private void GiveQuestReward()
        {
            // 퀘스트 완료 시 보상을 주는 로직을 작성합니다.
            // 여기에 플레이어 객체가 있다고 가정하고 보상을 플레이어에게 주는 등의 작업을 수행합니다.
            // 예를 들어, 플레이어에게 경험치나 게임 머니를 주는 등의 작업을 수행합니다.
        }
    }
}
