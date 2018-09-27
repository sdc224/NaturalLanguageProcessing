"""import speech_recognition as sr
r=sr.Recognizer() 
AUDIO_FILE = ("Shawn Mendes - Because I Had You.wav")
with sr.AudioFile(AUDIO_FILE) as source: 
    print("Listening:")
    audio=r.record(source)

try:
    print("the file contains : \n" + r.recognize_google(audio))
except:
    pass"""
#Python 2.x program to transcribe an Audio file 
import speech_recognition as sr 
#f=open("text.txt","w")
AUDIO_FILE = ("C:\\Users\\souro\\AppData\\Local\\Temp\\audio.wav") 
  
# use the audio file as the audio source 
  
r = sr.Recognizer() 
  
with sr.AudioFile(AUDIO_FILE) as source: 
    #reads the audio file. Here we use record instead of 
    #listen 
    audio = r.record(source)   
  
try: 
    s=r.recognize_google(audio)
    print(s)
   # f.write(s)
    #with open("text.txt","w") as f:
     #   f.write(s)
    #f.close()
  
except sr.UnknownValueError: 
    print("Google Speech Recognition could not understand audio") 
  
except sr.RequestError as e: 
    print("Could not request results from Google Speech")
