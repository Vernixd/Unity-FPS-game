using UnityEngine;
using System.Collections;

public class PowierzchniaTerenu : MonoBehaviour {
    
    private static float[] PobierzMixTekstur(Vector3 pozycjaGracza)
    {

        Terrain terrain = Terrain.activeTerrain;
  
        TerrainData terrainData = terrain.terrainData;

        Vector3 terrainPos = terrain.transform.position;

        
        int mapX = (int)(((pozycjaGracza.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((pozycjaGracza.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

     
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

        
        for (int n = 0; n < cellMix.Length; ++n)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }

        return cellMix;
    }

    
    public static string NazwaTeksturyWPozycji(Vector3 pozycjaGracza)
    {
        float[] mix = PobierzMixTekstur(pozycjaGracza);

        float maxMix = 0; 
        int maxIndex = 0; 

       
        for (int n = 0; n < mix.Length; ++n)
        {
            if (mix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = mix[n];
            }
        }

        
        SplatPrototype[] sp = Terrain.activeTerrain.terrainData.splatPrototypes;

        return sp[maxIndex].texture.name;
    }
}
