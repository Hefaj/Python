
# BCM (Binary Correlation Matrix) 
    Najprostsza pamięć autoasocjacyjna: 
 
    Cechy binarnej pamieci autoasocjacyjnej: 
    * Zdolność do rozpoznawania uszkodzonych wzorców – adresowalność kontekstowa. 
    * Czas nie zależy od liczby zapamiętanych wzorców. 
    * Uszkodzenie części macierzy połączeń nie prowadzi do zapomnienia konkretnych wzorców - brak lokalizacji. 
    * Interferencja (mylenie się) dla podobnych wzorców jest częstsza niż dla wzorców odmiennych. 
    * Przepełnienie pamięci (macierzy wag) prowadzi do chaotycznego zachowania. 


```python
import numpy as np 
 
class BCM(): 
    def __init__(self,X): 
        self.M = np.zeros((X[0],X[1])) 
 
    def fit(self,_x): 
        for i in xrange(len(_x)): 
            for j in xrange(len(_x)): 
                if _x[i] and _x[j]: 
                    self.M[i][j] = 1 
 
    def predict(self,_x,_t): 
        return  self.active( np.matrix(_x) * np.matrix(self.M), _t ) 
 
    def active(self,_x,_t): 
        _x = np.array(_x).ravel() 
        for n,i in enumerate(_x): 
            if i>_t: 
                _x[n]-=(_t) 
            elif i == _t: 
                _x[n] = 1 
            else: 
                _x[n] = 0 
 
        return _x 
 
 
a = np.array([1,1,0,0,0,0]) 
b = np.array([0,1,0,0,0,1]) 
 
B = BCM([6,6]) 
 
B.fit(a) 
B.fit(b) 
print B.predict(b,2) 
```

    [0. 1. 0. 0. 0. 1.]
    
