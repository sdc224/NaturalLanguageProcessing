#Python 2.x program to transcribe an Audio file 
try:
    import speech_recognition as sr 
except ModuleNotFoundError:
    import subprocess
    import sys    
    subprocess.check_call([sys.executable, '-m', 'pip', 'install', 'speech_recognition']) 
    import speech_recognition as sr

AUDIO_FILE = (str(sys.argv[1])) 
  
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
