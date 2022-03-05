using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayMusic : MonoBehaviour {
	[Header("Music File")]
	public AudioClip musicFile;
	[Range(0f, 1f)]
	public float volume = 1f;
	public bool isBumper = false;

	[Header("Trigger Settings")]
	public bool playOnAwake = false;
	public bool playOnTriggerEnter = false;
	public bool playOnTriggerExit = false;
	public string triggerTag = "Player";

	void Awake() {
		if (playOnAwake) PlayMusicFile();
	}

    void OnTriggerEnter(Collider other) {
        if (playOnTriggerEnter && other.gameObject.tag == triggerTag) PlayMusicFile();
	}

    void OnTriggerExit(Collider other) {
		if (playOnTriggerExit && other.gameObject.tag == triggerTag) PlayMusicFile();
	}

    public void PlayMusicFile() {
		if (!musicFile) return;

		MusicManager.CheckForManager();

		//Ask the music manager to play the track
		if (isBumper) {
			MusicManager.Instance.PlayBumper(musicFile, volume);
		}
		else {
			MusicManager.Instance.PlayMusic(musicFile, volume);
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayMusic))]
public class PlayMusicEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		if (!EditorApplication.isPlaying) return;

		if (GUILayout.Button("Play Music File")) {
			(target as PlayMusic).PlayMusicFile();
		}
	}
}
#endif

public class MusicManager : MonoBehaviour {
	public static MusicManager Instance;

	private AudioSource currentSource;
	private float currentSourceVolume;
	private bool bumperPlaying = false;

	static public void CheckForManager() {
		//If there is no music manager, make a new one
		if (!Instance) {
			GameObject freshGameObject = new GameObject();
			freshGameObject.name = "MusicManager";
			freshGameObject.AddComponent<MusicManager>();
		}
	}

	void Awake() {
		//Create a singleton
		if (!Instance) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public void PlayMusic(AudioClip newMusic, float volume = 1f) {
		//Store new volume
		currentSourceVolume = volume;

		//If it is the same music file at the same volume, do nothing
		if (currentSource && newMusic == currentSource.clip && volume == currentSource.volume) {
			return;
		}
		//If it is the same music file at a different volume, change the volume
		else if (currentSource && newMusic == currentSource.clip) {
			if (!bumperPlaying) StartCoroutine(FadeTo(currentSource, volume));
			return;
		}
		//If it is a new music file, and we are already playing music, fade out the old music
		else if (currentSource) {
			StartCoroutine(FadeAndStop(currentSource));
		}

		//Create a new audio source from scratch
		AudioSource freshsource = new GameObject().AddComponent<AudioSource>();

		//Set volume in context of bumper music
		if (bumperPlaying) {
			freshsource.volume = 0f;
		} else {
			freshsource.volume = volume;
		}

		//Assign all of our values to the audio source
		freshsource.loop = true;
		freshsource.playOnAwake = false;
		freshsource.gameObject.transform.parent = transform;
		freshsource.clip = newMusic;
		freshsource.gameObject.name = newMusic.name;
		freshsource.Play();


		//Keep track of the current music
		currentSource = freshsource;
	}

	public void PlayBumper(AudioClip bumperMusic, float volume = 1f) {
		//Dont interupt bumpers
		if (bumperPlaying) return;

		//Duck curresnt music
		if (currentSource) {
			StartCoroutine(FadeTo(currentSource, 0f));
		}

		//Create a new audio source from scratch
		AudioSource freshsource = new GameObject().AddComponent<AudioSource>();

		//Assign all of our values to the audio source
		freshsource.playOnAwake = false;
		freshsource.gameObject.transform.parent = transform;
		freshsource.clip = bumperMusic;
		freshsource.volume = volume;
		freshsource.gameObject.name = bumperMusic.name;
		freshsource.Play();

		//Schedual removal of bumber and return of music
		bumperPlaying = true;
		StartCoroutine(WaitAndBringCurrentSourceIn(bumperMusic.length, 3f));
		Destroy(freshsource.gameObject, bumperMusic.length);
	}

	public void StopMusic() {
		if (currentSource) {
			StartCoroutine(FadeAndStop(currentSource));
		}
	}

	IEnumerator FadeAndStop(AudioSource source) {
		float startTime = Time.time;
		float endTime = startTime + 0.15f;
		float startingVolume = source.volume;

		while (Time.time <= endTime) {
			source.volume = Mathf.Lerp(startingVolume, 0f, (Time.time - startTime) / 0.15f);
			yield return null;
		}

		source.volume = 0f;
		Destroy(source.gameObject);
	}

	IEnumerator FadeTo(AudioSource source, float newVolume, float fadeTime = 0.15f) {
		float startTime = Time.time;
		float endTime = startTime + fadeTime;
		float startingVolume = source.volume;

		while (Time.time <= endTime) {
			source.volume = Mathf.Lerp(startingVolume, newVolume, (Time.time - startTime) / fadeTime);
			yield return null;
		}

		source.volume = newVolume;
	}

	IEnumerator WaitAndBringCurrentSourceIn(float waitTime, float fadeTime = 1f) {
		float startTime = waitTime + Time.time;
		float endTime = startTime + fadeTime;

		while (Time.time < startTime) {
			yield return null;
		}

		while (Time.time <= endTime) {
			if (currentSource == null) {
				bumperPlaying = false;
				yield break;
			}

			currentSource.volume = Mathf.Lerp(0f, currentSourceVolume, (Time.time - startTime) / fadeTime);
			yield return null;
		}

		bumperPlaying = false;
		currentSource.volume = currentSourceVolume;
	}
}
