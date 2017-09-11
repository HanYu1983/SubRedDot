# 合作方法

## 資料夾的使用
Han: 工程師Han所使用  
Vic: 技美Vic所使用  
Lena: 美術師Lena所使用  
Resources: 專案需求所使用  
Resources/Output: 美術師所產生出遊戲中所使用的Prefab放置這裡  

#### 美術師使用範例請參照
1. Lena/Temp.unity
1. Resources/Output/TestPanel.prefab

## 專業範圍
Han: 非視覺與操做輸入的所有部分與上架流程  
Vic: 視覺互動與操做輸入的所有部分  
Lena: 所有視覺的Prefab，無論動態或靜態。這部分的工作量預估會最多，必須有純美術師幫忙（Lena算遊戲美術師了）  


#### 注意
不同專業之間重疊部分必須盡可能的少  

## 專業之間的關係
Lena產出視覺資源(Prefabs) -> Vic加入程式相關Component -> Han使用Vic的程式  
Lena和Vic之間可能會因為遊戲的邏輯需要而必須改變Prefab中的樹狀結構，這部分就會有互相來回的需要  
配合的方式大概是：

1. Lena依自己的判斷拉出畫面產生Prefab -> Vic加入程式相關Component發現結構上不太符合需要 -> Vic使Lena知道怎麼樣的結構比較好 -> Lean依新結構來調整 -> 循還
1. Lena不確定怎麼拉比較好 -> 告知Vic討論需求後Vic提出適當的結構 -> Lean依結構來調整 -> 循還

#### 注意
Lena與Vic之間因為很容易修改到同一個Prefab，會導致Git衝突。所以合作時必須非常注意檔案的修改權的交接。一定不要在同時間修改同一個Prefab