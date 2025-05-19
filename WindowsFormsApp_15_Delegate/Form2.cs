using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_15_Delegate
{
    //1. 일반 델리게이트
    public delegate void Notify();  //델리게이트 타입 정의

    //1) event 키워드 사용
    public delegate void Notify2();

    public delegate void EventDelegate();
    public partial class Form2 : Form
    {
        //2. 메서드 정의
        static void AlarmMessage()
        {
            Console.WriteLine("알람 울림");
        }
        public Form2()
        {
            InitializeComponent();

            Alarm alarm = new Alarm();

            Alarm2 alarm2 =new Alarm2();

            //4. 델리게이트 변수에 메서드 할당
            alarm.Onring += AlarmMessage;

            //4) Event 델리게이트 메서드 할당
            alarm2.OnRing += AlarmMessage;

            //5. 실행
            alarm.Onring(); //AlarmMessage 실행

            //5) Event 델리게이트 실행
            //alarm2.OnRing();    //컴파일 오류
            alarm2.Trigger();   //클래스 내부 메서드만 실행 가능 -> 안전
            
            //ex
            EventManager event1 = new EventManager();
            event1.addEvent("alarm", AlarmMessage);
        }
        //3. 클래스 정의
        public class Alarm
        {
            public Notify Onring;   //Notify 타입 변수 선언
        }
        //3) Event 클래스 정의
        public class Alarm2
        {
            public event Notify2 OnRing;  //Notify2 타입 변수 선언

            public void Trigger()
            {
                if (OnRing != null)
                {
                    OnRing();
                }
            }
        }

        //# 이벤트 메서드
        //- VS가 자동으로 "delegate 형식(EventHandler delegate)"에 맞춰서
        //만들어준 이벤트핸들러 메서드

        /*
         * Button 클래스 내부
         * public event EventHandler Click; //델리게이트 기반 이벤트
         * button.Click += new EventHandler(button1_Click);
         * 
         * 즉 button1_Click() 함수는 EventHandler라는 델리게이트에 연결되는 핸들러 함수
         * 
         * EventHandler
         * ㄴ .NET에서 미리 정의된 델리게이트 타입
         * 
         * object sender: 이벤트를 발생시킨 객체(ex. button1)
         * EvnetArgs e: 이벤트에 대한 정보
         * 
         * 1. 이미 정의된 이벤트(Button.Click 등)를 사용할 때
         * ㄴ이벤트에 연결할 메서드만 만들면 됨
         * 2. 직접 이벤트를 만들고 싶을 때 -> event 키워드 필요
         * 
         */

        //ex
        public class EventManager
        {
            IDictionary<string, EventDelegate> Event = new Dictionary<string, EventDelegate>();
            public void addEvent(string EventName, EventDelegate ED)
            {
                if (!Event.ContainsKey(EventName))
                {
                    Event.Add(EventName, ED);
                }
            }
            public void deleteEvent(string EventName)
            {
                if (Event.ContainsKey(EventName))
                {
                    Event.Remove(EventName);
                }
            }   
            public void startEvent(string EventName)
            {
                if (Event.ContainsKey(EventName))
                {
                    Event[EventName]?.Invoke();
                }
            }
        }

        //del() - 델리게이트 실행(축약)
        //del.Invoke() - 델리게이트 실행(정식)
        //del?.Invoke() - 등록된 함수가 존재할 경우에만 실행(null값 판별)
    }
}
