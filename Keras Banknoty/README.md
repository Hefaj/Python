
# Keras Banknoty 
### Fałszywe czy nie?


```python
import pandas as pd
```


```python
datas = pd.read_csv('https://archive.ics.uci.edu/ml/machine-learning-databases/ecoli/ecoli.data', index_col=False)
datas.columns = ['wariancja przekształconego obrazu Wavelet','pochylenie transformowanego obrazu Wavelet','kurtoza przekształconego obrazu','entropia obrazu','class']
datas
```




<div>
<style scoped>
    .dataframe tbody tr th:only-of-type {
        vertical-align: middle;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }

    .dataframe thead th {
        text-align: right;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>wariancja przekształconego obrazu Wavelet</th>
      <th>pochylenie transformowanego obrazu Wavelet</th>
      <th>kurtoza przekształconego obrazu</th>
      <th>entropia obrazu</th>
      <th>class</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>4.545900</td>
      <td>8.16740</td>
      <td>-2.458600</td>
      <td>-1.462100</td>
      <td>0</td>
    </tr>
    <tr>
      <th>1</th>
      <td>3.866000</td>
      <td>-2.63830</td>
      <td>1.924200</td>
      <td>0.106450</td>
      <td>0</td>
    </tr>
    <tr>
      <th>2</th>
      <td>3.456600</td>
      <td>9.52280</td>
      <td>-4.011200</td>
      <td>-3.594400</td>
      <td>0</td>
    </tr>
    <tr>
      <th>3</th>
      <td>0.329240</td>
      <td>-4.45520</td>
      <td>4.571800</td>
      <td>-0.988800</td>
      <td>0</td>
    </tr>
    <tr>
      <th>4</th>
      <td>4.368400</td>
      <td>9.67180</td>
      <td>-3.960600</td>
      <td>-3.162500</td>
      <td>0</td>
    </tr>
    <tr>
      <th>5</th>
      <td>3.591200</td>
      <td>3.01290</td>
      <td>0.728880</td>
      <td>0.564210</td>
      <td>0</td>
    </tr>
    <tr>
      <th>6</th>
      <td>2.092200</td>
      <td>-6.81000</td>
      <td>8.463600</td>
      <td>-0.602160</td>
      <td>0</td>
    </tr>
    <tr>
      <th>7</th>
      <td>3.203200</td>
      <td>5.75880</td>
      <td>-0.753450</td>
      <td>-0.612510</td>
      <td>0</td>
    </tr>
    <tr>
      <th>8</th>
      <td>1.535600</td>
      <td>9.17720</td>
      <td>-2.271800</td>
      <td>-0.735350</td>
      <td>0</td>
    </tr>
    <tr>
      <th>9</th>
      <td>1.224700</td>
      <td>8.77790</td>
      <td>-2.213500</td>
      <td>-0.806470</td>
      <td>0</td>
    </tr>
    <tr>
      <th>10</th>
      <td>3.989900</td>
      <td>-2.70660</td>
      <td>2.394600</td>
      <td>0.862910</td>
      <td>0</td>
    </tr>
    <tr>
      <th>11</th>
      <td>1.899300</td>
      <td>7.66250</td>
      <td>0.153940</td>
      <td>-3.110800</td>
      <td>0</td>
    </tr>
    <tr>
      <th>12</th>
      <td>-1.576800</td>
      <td>10.84300</td>
      <td>2.546200</td>
      <td>-2.936200</td>
      <td>0</td>
    </tr>
    <tr>
      <th>13</th>
      <td>3.404000</td>
      <td>8.72610</td>
      <td>-2.991500</td>
      <td>-0.572420</td>
      <td>0</td>
    </tr>
    <tr>
      <th>14</th>
      <td>4.676500</td>
      <td>-3.38950</td>
      <td>3.489600</td>
      <td>1.477100</td>
      <td>0</td>
    </tr>
    <tr>
      <th>15</th>
      <td>2.671900</td>
      <td>3.06460</td>
      <td>0.371580</td>
      <td>0.586190</td>
      <td>0</td>
    </tr>
    <tr>
      <th>16</th>
      <td>0.803550</td>
      <td>2.84730</td>
      <td>4.343900</td>
      <td>0.601700</td>
      <td>0</td>
    </tr>
    <tr>
      <th>17</th>
      <td>1.447900</td>
      <td>-4.87940</td>
      <td>8.342800</td>
      <td>-2.108600</td>
      <td>0</td>
    </tr>
    <tr>
      <th>18</th>
      <td>5.242300</td>
      <td>11.02720</td>
      <td>-4.353000</td>
      <td>-4.101300</td>
      <td>0</td>
    </tr>
    <tr>
      <th>19</th>
      <td>5.786700</td>
      <td>7.89020</td>
      <td>-2.619600</td>
      <td>-0.487080</td>
      <td>0</td>
    </tr>
    <tr>
      <th>20</th>
      <td>0.329200</td>
      <td>-4.45520</td>
      <td>4.571800</td>
      <td>-0.988800</td>
      <td>0</td>
    </tr>
    <tr>
      <th>21</th>
      <td>3.936200</td>
      <td>10.16220</td>
      <td>-3.823500</td>
      <td>-4.017200</td>
      <td>0</td>
    </tr>
    <tr>
      <th>22</th>
      <td>0.935840</td>
      <td>8.88550</td>
      <td>-1.683100</td>
      <td>-1.659900</td>
      <td>0</td>
    </tr>
    <tr>
      <th>23</th>
      <td>4.433800</td>
      <td>9.88700</td>
      <td>-4.679500</td>
      <td>-3.748300</td>
      <td>0</td>
    </tr>
    <tr>
      <th>24</th>
      <td>0.705700</td>
      <td>-5.49810</td>
      <td>8.336800</td>
      <td>-2.871500</td>
      <td>0</td>
    </tr>
    <tr>
      <th>25</th>
      <td>1.143200</td>
      <td>-3.74130</td>
      <td>5.577700</td>
      <td>-0.635780</td>
      <td>0</td>
    </tr>
    <tr>
      <th>26</th>
      <td>-0.382140</td>
      <td>8.39090</td>
      <td>2.162400</td>
      <td>-3.740500</td>
      <td>0</td>
    </tr>
    <tr>
      <th>27</th>
      <td>6.563300</td>
      <td>9.81870</td>
      <td>-4.411300</td>
      <td>-3.225800</td>
      <td>0</td>
    </tr>
    <tr>
      <th>28</th>
      <td>4.890600</td>
      <td>-3.35840</td>
      <td>3.420200</td>
      <td>1.090500</td>
      <td>0</td>
    </tr>
    <tr>
      <th>29</th>
      <td>-0.248110</td>
      <td>-0.17797</td>
      <td>4.906800</td>
      <td>0.154290</td>
      <td>0</td>
    </tr>
    <tr>
      <th>...</th>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
    </tr>
    <tr>
      <th>1341</th>
      <td>-1.747900</td>
      <td>-5.82300</td>
      <td>5.869900</td>
      <td>1.212000</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1342</th>
      <td>-0.959230</td>
      <td>-6.71280</td>
      <td>4.985700</td>
      <td>0.328860</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1343</th>
      <td>1.345100</td>
      <td>0.23589</td>
      <td>-1.878500</td>
      <td>1.325800</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1344</th>
      <td>2.227900</td>
      <td>4.09510</td>
      <td>-4.803700</td>
      <td>-2.111200</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1345</th>
      <td>1.257200</td>
      <td>4.87310</td>
      <td>-5.286100</td>
      <td>-5.874100</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1346</th>
      <td>-5.385700</td>
      <td>9.12140</td>
      <td>-0.419290</td>
      <td>-5.918100</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1347</th>
      <td>-2.978600</td>
      <td>2.34450</td>
      <td>0.526670</td>
      <td>-0.401730</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1348</th>
      <td>-1.585100</td>
      <td>-2.15620</td>
      <td>1.708200</td>
      <td>0.901700</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1349</th>
      <td>-0.218880</td>
      <td>-2.20380</td>
      <td>-0.095400</td>
      <td>0.564210</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1350</th>
      <td>1.318300</td>
      <td>1.90170</td>
      <td>-3.311100</td>
      <td>0.065071</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1351</th>
      <td>1.489600</td>
      <td>3.42880</td>
      <td>-4.030900</td>
      <td>-1.425900</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1352</th>
      <td>0.115920</td>
      <td>3.22190</td>
      <td>-3.430200</td>
      <td>-2.845700</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1353</th>
      <td>-3.392400</td>
      <td>3.35640</td>
      <td>-0.720040</td>
      <td>-3.523300</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1354</th>
      <td>-6.163200</td>
      <td>8.70960</td>
      <td>-0.216210</td>
      <td>-3.634500</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1355</th>
      <td>-4.078600</td>
      <td>2.92390</td>
      <td>0.870260</td>
      <td>-0.653890</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1356</th>
      <td>-2.589900</td>
      <td>-0.39110</td>
      <td>0.934520</td>
      <td>0.429720</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1357</th>
      <td>-1.011600</td>
      <td>-0.19038</td>
      <td>-0.905970</td>
      <td>0.003003</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1358</th>
      <td>0.066129</td>
      <td>2.49140</td>
      <td>-2.940100</td>
      <td>-0.621560</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1359</th>
      <td>-0.247450</td>
      <td>1.93680</td>
      <td>-2.469700</td>
      <td>-0.805180</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1360</th>
      <td>-1.573200</td>
      <td>1.06360</td>
      <td>-0.712320</td>
      <td>-0.838800</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1361</th>
      <td>-2.166800</td>
      <td>1.59330</td>
      <td>0.045122</td>
      <td>-1.678000</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1362</th>
      <td>-1.166700</td>
      <td>-1.42370</td>
      <td>2.924100</td>
      <td>0.661190</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1363</th>
      <td>-2.839100</td>
      <td>-6.63000</td>
      <td>10.484900</td>
      <td>-0.421130</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1364</th>
      <td>-4.504600</td>
      <td>-5.81260</td>
      <td>10.886700</td>
      <td>-0.528460</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1365</th>
      <td>-2.410000</td>
      <td>3.74330</td>
      <td>-0.402150</td>
      <td>-1.295300</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1366</th>
      <td>0.406140</td>
      <td>1.34920</td>
      <td>-1.450100</td>
      <td>-0.559490</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1367</th>
      <td>-1.388700</td>
      <td>-4.87730</td>
      <td>6.477400</td>
      <td>0.341790</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1368</th>
      <td>-3.750300</td>
      <td>-13.45860</td>
      <td>17.593200</td>
      <td>-2.777100</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1369</th>
      <td>-3.563700</td>
      <td>-8.38270</td>
      <td>12.393000</td>
      <td>-1.282300</td>
      <td>1</td>
    </tr>
    <tr>
      <th>1370</th>
      <td>-2.541900</td>
      <td>-0.65804</td>
      <td>2.684200</td>
      <td>1.195200</td>
      <td>1</td>
    </tr>
  </tbody>
</table>
<p>1371 rows × 5 columns</p>
</div>




```python
print datas.isnull().sum()
```

    wariancja przekształconego obrazu Wavelet     0
    pochylenie transformowanego obrazu Wavelet    0
    kurtoza przekształconego obrazu               0
    entropia obrazu                               0
    class                                         0
    dtype: int64
    


```python
from sklearn.preprocessing import scale
X, y = datas.values[:,:-1], datas.values[:,-1]
X_sc = scale(X)
```


```python
from keras.utils import to_categorical
```


```python
y_ohe = to_categorical(y)
y_ohe
```




    array([[1., 0.],
           [1., 0.],
           [1., 0.],
           ...,
           [0., 1.],
           [0., 1.],
           [0., 1.]])




```python
from keras.models import Sequential
from keras.layers import Dense
```


```python
model = Sequential()
model.add(Dense(5,input_shape=(4,) , activation='relu'))
model.add(Dense(y_ohe.shape[1], activation='softmax'))
```


```python
model.compile(optimizer='Adam', 
              metrics=['accuracy'], 
              loss='categorical_crossentropy')
```


```python
#Adam
model.fit(X_sc,y_ohe,batch_size=30, epochs=20, verbose=2)
```

    Epoch 1/20
     - 1s - loss: 1.0588 - acc: 0.1933
    Epoch 2/20
     - 1s - loss: 0.9129 - acc: 0.3698
    Epoch 3/20
     - 1s - loss: 0.8028 - acc: 0.4945
    Epoch 4/20
     - 1s - loss: 0.7156 - acc: 0.5208
    Epoch 5/20
     - 1s - loss: 0.6481 - acc: 0.5682
    Epoch 6/20
     - 1s - loss: 0.5934 - acc: 0.6565
    Epoch 7/20
     - 1s - loss: 0.5450 - acc: 0.7250
    Epoch 8/20
     - 1s - loss: 0.4990 - acc: 0.7856
    Epoch 9/20
     - 1s - loss: 0.4554 - acc: 0.8308
    Epoch 10/20
     - 1s - loss: 0.4158 - acc: 0.8636
    Epoch 11/20
     - 1s - loss: 0.3807 - acc: 0.8928
    Epoch 12/20
     - 1s - loss: 0.3494 - acc: 0.9110
    Epoch 13/20
     - 1s - loss: 0.3215 - acc: 0.9322
    Epoch 14/20
     - 1s - loss: 0.2968 - acc: 0.9482
    Epoch 15/20
     - 1s - loss: 0.2750 - acc: 0.9606
    Epoch 16/20
     - 1s - loss: 0.2555 - acc: 0.9672
    Epoch 17/20
     - 1s - loss: 0.2382 - acc: 0.9708
    Epoch 18/20
     - 1s - loss: 0.2227 - acc: 0.9723
    Epoch 19/20
     - 1s - loss: 0.2087 - acc: 0.9723
    Epoch 20/20
     - 1s - loss: 0.1962 - acc: 0.9767
    




    <keras.callbacks.History at 0xef086f0>




```python
score = model.evaluate(X_sc, y_ohe)
str(score[1]*100)+"%"
```

    1371/1371 [==============================] - 0s 155us/step
    




    '97.73887677578749%'




```python
y_pred = model.predict(X_sc)
```


```python
from sklearn.metrics import confusion_matrix
import numpy as np
```


```python
matrix = confusion_matrix(y, np.argmax(y_pred,axis=1))
```


```python
print matrix
```

    [[742  19]
     [ 12 598]]
    
