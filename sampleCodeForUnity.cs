using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SubmitRank("fdsafdsa","maro",333);

    }


    //랭킹 출력 관련.
    //start = 시작 등수(1이면 1등부터...)
    //length = 한 번에 몇 명을 보여줄 것인지(20이면 20명)
    //기본 값으로 1등부터 20명을 보여줍니다.
    void PrintRank(int start = 1, int length = 20)
    {
        WWW www = new WWW("http://10.10.0.11/api/ranking/getRank?start=" + start + "&length=" + length);
        while (true)
        {
            if (www.isDone) break;
        }
        if (www.error == null)
        {
            Debug.Log(www.text);
            JsonData jsonvale = JsonMapper.ToObject(www.text);
            for (int i = 0; i < jsonvale["ranking"].Count; i++)
            {
                //↓이 곳을 수정하여 화면에 랭킹을 뿌려주시면 됩니다.
                //↓이 곳을 수정하여 화면에 랭킹을 뿌려주시면 됩니다.
                //↓이 곳을 수정하여 화면에 랭킹을 뿌려주시면 됩니다.
                Debug.Log(jsonvale["ranking"][i]["id"]); //사용자 식별(중복되면 가장 높은 점수만 나오겠죠?)
                Debug.Log(jsonvale["ranking"][i]["name"]); //사용자 이름
                Debug.Log(jsonvale["ranking"][i]["score"]); //점수
                //↑이 곳을 수정하여 화면에 랭킹을 뿌려주시면 됩니다.
                //↑이 곳을 수정하여 화면에 랭킹을 뿌려주시면 됩니다.
                //↑이 곳을 수정하여 화면에 랭킹을 뿌려주시면 됩니다.
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
        }
    }


    //등급컷 출력 관련.
    //1등급부터 9등급까지의 정보를 뿌려줍니다.
    void PrintGrade()
    {
        WWW www = new WWW("http://10.10.0.11/api/ranking/getRank");
        while (true)
        {
            if (www.isDone) break;
        }
        if (www.error == null)
        {
            Debug.Log(www.text);
            JsonData jsonvale = JsonMapper.ToObject(www.text);
            for (int i = 0; i < jsonvale["gradeCut"].Count; i++)
            {
                //↓이 곳을 수정하여 화면에 등급컷을 뿌려주시면 됩니다.
                //↓이 곳을 수정하여 화면에 등급컷을 뿌려주시면 됩니다.
                //↓이 곳을 수정하여 화면에 등급컷을 뿌려주시면 됩니다.
                Debug.Log(jsonvale["gradeCut"][i]["grade"]); //몇 등급인지... 1등급, 2등급, 3등급, ... , 9등급.
                Debug.Log(jsonvale["gradeCut"][i]["rank"]); //몇 등 이상이 해당 등급인 지...
                Debug.Log(jsonvale["gradeCut"][i]["score"]); //등급 컷 원점수
                //↑이 곳을 수정하여 화면에 등급컷을 뿌려주시면 됩니다.
                //↑이 곳을 수정하여 화면에 등급컷을 뿌려주시면 됩니다.
                //↑이 곳을 수정하여 화면에 등급컷을 뿌려주시면 됩니다.
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
        }
    }

    //랭킹을 입력합니다
    //id: 사용자를 식별할 수 있는 정보(이메일, IMEI, 전화번호 등... 중복으로 입력이 되어도 높은 점수만 출력되게 하기 위함입니다.)
    //name: 이름
    //score: 점수!
    void SubmitRank(string id, string name, int score)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("id", id);
        formData.AddField("name", name);
        formData.AddField("score", score);
        WWW www = new WWW("http://10.10.0.11/api/ranking/submit", formData);
        while (true)
        {
            if (www.isDone) break;
        }
        if (www.error == null)
        {
            Debug.Log(www.text);
            JsonData jsonvale = JsonMapper.ToObject(www.text);
            if (jsonvale["code"].ToString() == "200")
            {
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
                //성공하였을 경우 액션!!!!
            }
            else
            {
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
                //실패하였을 경우 액션!!!!
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!
            //에러났을 경우 액션!!!!

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
