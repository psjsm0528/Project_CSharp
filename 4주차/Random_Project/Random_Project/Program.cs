namespace Random_Project
{
    internal class Program
    {
        /* (1) seed값. 한번 코드를 실행한번만 다시 시작하는 코드가 동일합니다.
         * => GenerateRandom 코드를 수정해서 실행할 때 다른 값이 나오도록 변경해보세요.
         * 
         * (2) Item의 데이터를 몇 가지 더 추가해보세요.
         * 
         * (3) Rank의 확률을 조정해서 특정 아이템이 잘 안나오도록 밸런싱 해보세요.
         *
         *=> item을 상속하는 특별한 클래스를 만들어서 가챠 시스템을 유지한 상태로 코드를 구현해보세요.
         *=> 솔루션 탐색기 - 마우스 우클릭 - 클래스 생성
         * class character : Iten
         */

        static void Main(string[] args)
        {
            // 아이템 뽑기 클래스를 가져오겠다.
            Gatcha gatcha = new Gatcha();

            gatcha.GenerateRandom();
            gatcha.GenerateProbability();
            gatcha.SetTable();
            gatcha.pick();

            gatcha.Pick10Time();
        }
    }
}
