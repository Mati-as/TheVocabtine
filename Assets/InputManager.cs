using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase;
using System.Text.RegularExpressions;
using Firebase.Extensions;
using TMPro;
using UniRx;
using UnityEngine.Serialization;


public class InputManager : MonoBehaviour
{
    private string username;
    
    public Button userSubmittedButton;
    public TextMeshProUGUI userInputTmp;
    [FormerlySerializedAs("userInputString")] public string userInputWord;
    public TMP_InputField wordField;
    public TMP_InputField userIDField;

    public Button inputButton;
    // Firebase의 데이터베이스 참조
    private DatabaseReference databaseReference;

    private void Start()
    {
         // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // FirebaseApp 인스턴스에 액세스
                var app = FirebaseApp.DefaultInstance;

                // 데이터베이스 참조 초기화
                databaseReference = FirebaseDatabase.GetInstance(app).RootReference.Child("users");
                
                //databaseReference = FirebaseDatabase.DefaultInstance
                 //   .GetReferenceFromUrl("https://my-first-database-with-unity-default-rtdb.firebaseio.com/");
                
                
                Debug.Log("database initialized.");
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
       
        // what's the difference between rootreference? 
       
        
        
        userSubmittedButton.OnClickAsObservable()
            .Subscribe(_ => OnButtonClicked())
            .AddTo(this); // 객체가 파괴될 때 구독 해제를 위해 AddTo 사용
    }

    

    
    void OnButtonClicked()
    {
        Debug.Log("Button was clicked!");
        OnSubmitButtonClicked();
    }


    
    public void OnSubmitButtonClicked()
    {
        username = userIDField.text.Trim();
        RegisterIDAndAddWord();
        
        userInputWord = wordField.text.Trim();
        
       if (databaseReference == null) // 초기화 확인
       {
           Debug.LogError("Database reference is not initialized yet.");
           return; // 초기화되지 않았으면 작업 중단
       }
       
       //string jsonData = JsonUtility.ToJson(userInputWord);
        
       // databaseReference.SetValueAsync(userInputWord);
       // databaseReference.Child(username).SetRawJsonValueAsync(jsonData);

        userInputTmp.text = "저장";
        wordField.text = "저장";
        Debug.Log("Saved");
        // string processedText = RemoveNumbers(userInputTmp.text);
        // SaveToFirebase(processedText);
        

     

    }

    public void OnDataLoadButtonClicked()
    {
        //username = usernameField.text.Trim();
            
        databaseReference.Child(username).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                userInputTmp.text = "save canceled.";
            }

            else if (task.IsFaulted)
            {
                userInputTmp.text = "save failed.";
            }

            else
            {
                var dataSnapshot = task.Result;
                string dataString = "";
                foreach (var data in dataSnapshot.Children)
                {
                    dataString += data.Key + "" + data.Value + "\n";
                }
                
                userInputTmp.text = dataString;
            }
        
        });

    }

    private string RemoveNumbers(string input)
    {
        return Regex.Replace(input, "[0-9]", "");
    }
    
    
    
    

    public string userInputPath = "userInput";
    private void SaveToFirebase(string data)
    {
       
           string key = databaseReference.Child(userInputPath).Push().Key;
           databaseReference.Child(userInputPath).Child(key).SetValueAsync(data);
        
    }
    
    public void RegisterIDAndAddWord()
    {
        string potentialUsername = userIDField.text.Trim(); // 사용자가 입력한 username

        // 해당 username이 이미 있는지 확인
        databaseReference.Child(potentialUsername).GetValueAsync().ContinueWith(task => 
        {
            if (task.IsCompleted) 
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists) 
                {
                    // username이 이미 존재, 단어만 추가
                    AddWordToUsername(potentialUsername);
                } 
                else 
                {
                    // username 등록 및 단어 추가
                    RegisterUsername(potentialUsername);
                }
            }
        });
    }

    public void RegisterUsername(string newUsername)
    {
       
        // username 등록
        DatabaseReference userRef = databaseReference.Child(newUsername);
        
        userRef.SetValueAsync(newUsername);
        // 첫 단어 추가
        userRef.Child("word1").SetValueAsync(userInputWord);
        
    }

    public void AddWordToUsername(string existingUsername)
    {
        // 기존 username 아래에 새로운 단어 추가
        DatabaseReference userRef = databaseReference.Child(existingUsername);
        string newWordKey = userRef.Push().Key;
        userRef.Child(newWordKey).SetValueAsync(userInputWord);
    }
    
}



public static class ButtonEventExtension
{
    public static IObservable<Unit> OnClickAsObservable(this Button button)
    {
        return Observable.Create<Unit>(observer =>
        {
            button.onClick.AddListener(() => observer.OnNext(Unit.Default));
            return Disposable.Create(() => button.onClick.RemoveListener(() => observer.OnNext(Unit.Default)));
        });
    }
}

