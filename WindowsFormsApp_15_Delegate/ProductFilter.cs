using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp_15_Delegate
{
    public class ProductFilter
    {
        //조건 델리게이트 타입 정의
        public delegate bool ProductCondition(Product p);
        //= Product 타입 개체를 전달받아 bool(true/false)을 반환하는 함수 형식
        //=> 즉, 조건을 검사하는 용도로 사용
        
        //정적 메서드 정의
        public static List<Product> Filter(List<Product> products, ProductCondition condition)
        //ㄴ static: 객체를 만들지 않고도 클래스 이름으로 직접 호출 가능
        //ㄴ List<Product>: 이 메서드는 Product 리스트를 반환
        //ㄴ 매개변수
        //1) List<Product> products: 전체 제품 목록(필터링 대상)
        //2) ProductCondition condition: 조건 함수 (델리게이트로 전달받음)
        //제품 리스트 중 조건을 만족하는 항목들만 필터링해서 반환하는 메서드
        {
            List<Product> result = new List<Product>();
            //조건을 만족하는 제품만 따로 저장할 결과 리스트를 미리 생성
            foreach (var p in products)
            {
                if (condition(p))
                    result.Add(p);
            }

            return result;
        }
    }
}
