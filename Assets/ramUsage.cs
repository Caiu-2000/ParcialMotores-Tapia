using TMPro;

using UnityEngine;
using UnityEngine.Profiling;

#if UNITY_EDITOR
public class ramUsage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugRam;
    public static ramUsage instance;

    private void Start()


    {
        if (instance == null) instance = this;
        else return;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // Total RAM allocated by Unity for your game (in bytes)
        long allocatedMemory = Profiler.GetTotalAllocatedMemoryLong();

        // Total RAM reserved by the system for Unity (allocated + unallocated pool)
        long reservedMemory = Profiler.GetTotalReservedMemoryLong();

        // Convert to Megabytes for readability
        double allocatedMB = allocatedMemory / (1024.0 * 1024.0);
        double reservedMB = reservedMemory / (1024.0 * 1024.0);

        debugRam.text = "Allocated RAM: " + allocatedMB + " Reserved RAM: " + reservedMB;

    }

}

#endif