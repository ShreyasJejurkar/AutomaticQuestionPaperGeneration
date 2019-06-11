from nltk.tokenize import sent_tokenize, word_tokenize
from nltk.corpus import stopwords,wordnet
from nltk.stem import WordNetLemmatizer
from itertools import product
import numpy
from flask import Flask
from flask import request
from flask import jsonify
import json

app = Flask(__name__)

@app.route("/")
def hello():
    return "Hello World!"

@app.route("/semantic", methods=['POST'])
def Calculate():
    request_json = request.get_json()
    
    value1 = request_json.get('first_text')
    value2 = request_json.get('second_text')

    stop_words = set(stopwords.words("english"))
    filtered_sentence1 = []
    filtered_sentence2 = [] 
    lemm_sentence1 = []
    lemm_sentence2 = []
    sims = []
    temp1 = []
    temp2 = []
    simi = []
    final = []
    same_sent1 = []
    same_sent2 = []


##---------------Defining WordNet Lematizer for English Language---------------##
    lemmatizer  =  WordNetLemmatizer()
    
    ##---------------Tokenizing and removing the Stopwords---------------##

    for words1 in word_tokenize(value1):
        if words1 not in stop_words:
            if words1.isalnum():
                filtered_sentence1.append(words1)

    ##---------------Lemmatizing: Root Words---------------##

    for i in filtered_sentence1:
        lemm_sentence1.append(lemmatizer.lemmatize(i))
        
    #print(lemm_sentence1)


    ##---------------Tokenizing and removing the Stopwords---------------##

    for words2 in word_tokenize(value2):
        if words2 not in stop_words:
            if words2.isalnum():
                filtered_sentence2.append(words2)

    ##---------------Lemmatizing: Root Words---------------##

    for i in filtered_sentence2:
        lemm_sentence2.append(lemmatizer.lemmatize(i))
        
    #print(lemm_sentence2)

    ##---------------Removing the same words from the tokens----------------##
    ##for word1 in lemm_sentence1:
    ##    for word2 in lemm_sentence2:
    ##        if word1 == word2:
    ##            same_sent1.append(word1)
    ##            same_sent2.append(word2)
    ##            
    ##if same_sent1 != []:
    ##   for word1 in same_sent1:
    ##    lemm_sentence1.remove(word1)
    ##if same_sent2 != []:
    ##   for word2 in same_sent2:
    ##    lemm_sentence2.remove(word2)
    ##            
    ##print(lemm_sentence1)
    ##print(lemm_sentence2)

    ##---------------Similarity index calculation for each word---------------##
    for word1 in lemm_sentence1:
        simi =[]
        for word2 in lemm_sentence2:
            sims = []
        # print(word1)
            #print(word2)
            syns1 = wordnet.synsets(word1)
            #print(syns1)
            #print(wordFromList1[0])
            syns2 = wordnet.synsets(word2)
            #print(wordFromList2[0])
            for sense1, sense2 in product(syns1, syns2):
                d = wordnet.wup_similarity(sense1, sense2)
                if d != None:
                    sims.append(d)
        
            #print(sims)
            #print(max(sims))
            max_sim = 0
            if sims != []:
                max_sim = max(sims)
            #print(max_sim)
            simi.append(max_sim)
                
        if simi != []:
            max_final = max(simi)
            final.append(max_final)

    similarity_index = numpy.mean(final)
    similarity_index = round(similarity_index , 2)
    print (similarity_index)

    #Server response
    return jsonify(
        FirstText = value1,
        SecondText = value2,
        SemanticScore = similarity_index
    )
if __name__ == "__main__":
    app.run(port=5000,debug=True)
