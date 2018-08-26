import numpy as np
from scipy.special import expit
import sys
import pandas as pd
import matplotlib.pyplot as plt
from time import time
from keras.datasets import mnist # pobieranie zestawu danych MNIST
from sklearn.metrics import confusion_matrix

def wine_load():
    w = pd.read_csv('https://archive.ics.uci.edu/ml/machine-learning-databases/wine/wine.data', header=None).values
    np.random.shuffle(w)
    return w[:150,1:], np.array(map(int, w[:150,0]-1)) , w[150:,1:],np.array(map(int, w[150:,0]-1))

def conf_matrix(y_test, y, c=None,i=None):
    matrix = confusion_matrix(y_test, y)
    return pd.DataFrame(data=matrix, columns=c, index=i)

def pred_score(X_train, y_train, X_test, y_test, mlp):
    x = mlp.predict(X_train)
    print "Dane treningowe: %.2f%%" % ((x==y_train).sum()/float(len(y_train))*100)
    y = mlp.predict(X_test)
    print "Dane testowe: %.2f%%" % (np.sum(y==y_test, axis=0)/float(X_test.shape[0])*100)

def print_plot(mlp):
    
    cout_=np.array_split(range(len(mlp.cost_)),mlp.epochs) 
    cost_ = [np.mean(np.array(mlp.cost_)[i]) for i in cout_]
    
    plt.plot(range(len(cost_)),cost_)
    plt.ylabel('Koszt')
    plt.xlabel('Epoka')
    plt.tight_layout()
    plt.show()

def print_plots(X_test, y_test=0, y=0, show=False, r=5, c=5):

    if show:
        z1 = X_test[y!=y_test][:r*c]
        z2 = y_test[y!=y_test][:r*c]
        z3 = y[y!=y_test][:r*c]
    else:
        z1 = X_test[:r*c]
    
    fig, ax = plt.subplots(nrows=r,
                           ncols=c,
                           sharex=True,
                           sharey=True)
    ax = ax.flatten()
    for i in range(r*c):
        ax[i].imshow(z1[i].reshape(28,28), cmap='Greys', interpolation='nearest')

        if show:
            ax[i].set_title('T: %s, P: %s' % (z2[i], z3[i]))
        ax[0].set_xticks([])
        ax[0].set_yticks([])

    plt.tight_layout()
    plt.show()

def importMNIST():     
    (X_train, y_train), (X_test, y_test) = mnist.load_data()  
    X_train, X_test = X_train.reshape(X_train.shape[0], 28*28), X_test.reshape(X_test.shape[0], 28*28)
    return (X_train, y_train), (X_test, y_test)

class MLP():
   # konstruktor
    def __init__(self, n_output, n_features, n_hidden=30,
                 l1=0.0, l2=0.0, epochs=500, eta=0.001, 
                alpha=0.0, eta_val=0.0, shuffle=True,
                batches=1, nameCostFunction='meanssquare'):   
        """
        self.n_output    - ilosc etykiet
        self.n_features  - ilosc cech
        self.n_hidden    - ilosc neuronow w warstwie ukrytej
        self.l1          - wspolczynnik regularyzacji l1
        self.l2          - wspolczynnik regularyzacji l2
        self.epochs      - ilosc epok
        self.eta         - wspolczynnik zmiany wagi
        self.eta_val     - wspolczynnik zmiany eta
        self.batches     - rozdzielenie danych na k zbiorow w kazdej epoce, gradient jest liczony dla kazdego zbioru
        self.aplha       - wpsolczynnik momentu
        self.shuffle     - czy wektory maja byc mieszane co kazda epoke
        self.w1, self.w2 - wagi warstwy wejsciowej i ukrytej
        self.nameCost =  - rodzaj funkcji kosztu ktora ma byc uzyta
        """
        self.n_output = n_output
        self.n_features = n_features
        self.n_hidden = n_hidden
        self.l1 = l1
        self.l2 = l2
        self.epochs = epochs
        self.eta = eta
        self.eta_val = eta_val
        self.batches = batches
        self.alpha = alpha
        self.shuffle = shuffle
        self.w1, self.w2 = self._init_weights()
        self.nameCostFunction = nameCostFunction
        np.random.seed(1)
    
    # zapisanie wag do csv
    def save_w(self, name1='w1.csv', name2='w2.csv'):
        pd.DataFrame(self.w1).to_csv(name1)
        pd.DataFrame(self.w2).to_csv(name2)
    
    # zaladowanie wag z csv
    def load_w(self, name1='w1.csv', name2='w2.csv'):
        self.w1 = pd.read_csv(name1,sep=',').values[:,1:]
        self.w2 = pd.read_csv(name2,sep=',').values[:,1:]
        
    # one hot encoding
    def ohe(self, y,n):
        ohe = np.zeros((n,y.shape[0]))
        for i, v in enumerate(y):
            ohe[v,i] = 1.0
        return ohe
    
    # inicjacja wag
    def _init_weights(self):
        w1 = np.random.uniform(-1.0, 1.0, size=self.n_hidden*(self.n_features + 1))
        w1 = w1.reshape(self.n_hidden, self.n_features + 1)
        w2 = np.random.uniform(-1.0, 1.0, size=self.n_output*(self.n_hidden + 1))
        w2 = w2.reshape(self.n_output, self.n_hidden + 1)
        return w1, w2
        
    # funkcja aktywacji
    def _sigmoid(self,z):
        return expit(z)
    
    # pochodna aktywacji
    def _sigmoid_gradient(self,z):
        sig = self._sigmoid(z)
        return sig * (1-sig)
    
    # zmniejszenie stopnia przetrenowania za pomoca L1 i L2
    # regularyzacja L2 
    def _L2(self, lamb_, w1, w2):
        return (lamb_/2.0) * (np.sum(w1[:, 1:] ** 2) + np.sum(w2[:, 1:] ** 2))
    
    # regularyzacja L1
    def _L1(self, lamb_, w1, w2):
        return (lamb_/2.0) * (np.abs(w1[:,1:]).sum() + np.abs(w2[:,1:]).sum())
    
    # dodanie biasu do wejscia
    def _add_bias(self, X,a='c'):
        if a == 'c':
            X_n = np.ones((X.shape[0], X.shape[1] + 1))
            X_n[:, 1:] = X
        elif a == 'r':
            X_n = np.ones((X.shape[0] + 1, X.shape[1]))
            X_n[1:, :] = X
        return X_n
    
    # logistyczna funkcja kosztu
    def _get_cost(self, y_ohe, output, w1, w2, nameCost):
        
        if nameCost == 'logistic':
            t1 = -y_ohe * (np.log(output))
            t2 = (1 -  y_ohe) * np.log(1-output)
            cost = np.sum(t1 - t2)
        elif nameCost == 'meanssquare':
            cost = np.sum((y_ohe - output)**2)/2.0
            
        L1_t = self._L1(self.l1, w1,w2)
        L2_t = self._L2(self.l2, w1,w2)
        cost = cost + L1_t + L2_t
        return cost
    
    # forward propagation
    def _forward(self,X, w1, w2):
        a1 = self._add_bias(X, a='c')
        z2 = w1.dot(a1.T)
        a2 = self._sigmoid(z2)
        a2 = self._add_bias(a2, a='r')
        z3 = w2.dot(a2)
        a3 = self._sigmoid(z3)
        return a1, z2, a2, z3, a3
    
    
    # back propagation
    def _backpropagation(self,a1,a2,a3,z2,y_ohe,w1,w2):
        """
            a1 - wektor wejsciowy z dodanym biasek
            a2 - wektor wyjsciowy z pierwszej warstwy
            a3 - wektor wyjsciowy z warstwy ukrytej -> wyjscie modelu
            z2 - wektor wejsciowy * wektor wag w1
            y_ohe - oczekiwanie wyjscie w one hot encoding
            w1 - wagi pierszej warstwy
            w2 - wagi drugiej warstwy
        """
        delta = a3 - y_ohe
        z2 = self._add_bias(z2,a='r')
        delta2 = w2.T.dot(delta) * self._sigmoid_gradient(z2)
        delta2 = delta2[1:,:]
        grad1 = delta2.dot(a1)
        grad2 = delta.dot(a2.T)
        
        # regularyzacja L1, L2
        grad1[:, 1:] += self.l2 * w1[:, 1:]
        grad1[:, 1:] += self.l1 * np.sign(w1[:, 1:])
        grad2[:, 1:] += self.l2 * w2[:, 1:]
        grad2[:, 1:] += self.l1 * np.sign(w2[:, 1:])
        
        return grad1, grad2
    
    def predict(self, X):
        a1,z2,a2,z3,a3 = self._forward(X, self.w1, self.w2)
        y_predict = np.argmax(z3,axis=0)
        return y_predict
    
    def fit(self, X,y):
        self.cost_ = []
        
        t = time()
        
        X_data, y_data = X.copy(), y.copy()
        y_ohe = self.ohe(y,self.n_output)
        
        delta_w1_old, delta_w2_old = np.zeros(self.w1.shape), np.zeros(self.w2.shape)
        
        for i in xrange(self.epochs):
            # uspolczynnik uczenia bedzie malal z uplywem epok
            self.eta /= (1 + self.eta_val*i)
            
            sys.stderr.write('\rEpoka: %s/%s, time: %d.%dm' %  ((i+1),self.epochs, (time()-t)/60, (time()-t)%60))
            #sys.stderr.flush()
            #print ('Epoka %s/%s' %  ((i+1),self.epochs) )
            
            if self.shuffle:
                np.random.shuffle(zip(X_data,y_ohe))
                
            batch = np.array_split(range(y_data.shape[0]),self.batches)
            # przetwarzanie po k wektorow
            for idx in batch:               
                a1, z2, a2, z3, a3 = self._forward(X_data[idx], self.w1, self.w2)
                cost = self._get_cost(y_ohe=y_ohe[:,idx], output=a3, w1=self.w1, w2=self.w2, nameCost=self.nameCostFunction)
                self.cost_.append(cost)
                
                # obliczanie gradientu
                grad1, grad2 = self._backpropagation(a1=a1,a2=a2,a3=a3,z2=z2,
                                                     y_ohe=y_ohe[:,idx],w1=self.w1,w2=self.w2)
                
                # zmiana wag
                delta_w1, delta_w2 = self.eta * grad1, self.eta * grad2 
                
                self.w1 -= (delta_w1 + (self.alpha * delta_w1_old))
                self.w2 -= (delta_w2 + (self.alpha * delta_w2_old))
                
                delta_w1_old, delta_w2_old = delta_w1, delta_w2
            
        # zapisanie wag na wypadek nauczenia i wylaczenia 
        self.save_w()
        
        return self