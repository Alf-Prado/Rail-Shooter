// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "My First Shader"
{

		Properties {
      		_MainTex ("Texture", 2D) = "white" {}
    	}
		SubShader{
			
		  Tags { "RenderType" = "Opaque" }
		  CGPROGRAM
		  #pragma surface surf Lambert vertex:vert
		
			struct Input {
				float2 uv_MainTex;
			};

		// Access the shaderlab properties
			sampler2D _MainTex;

			// Vertex modifier function
			void vert(inout appdata_full v) {
				
					float inclinations[36] = {5.1885, 0.936158, 0.0576206, 1.15097, 4.38816, 3.90341, 1.88837, 0.342738, 4.19601, 2.03696, 5.25573, 5.89031, 3.16097, 
						4.10351, 2.01757, 2.05049, 3.77202, 0.221111, 0.377759, 0.226108, 3.95581, 0.879102, 0.335845, 0.668324, 3.59287, 2.98621, 1.59642, 6.26446, 
						1.61655, 1.81525, 1.7865, 0.629079, 3.48038, 2.7052, 5.51847, 0.420369}; 

					float azimuth[36] = {3.15554, 4.76413, 2.39323, 3.38041, 5.89175, 0.353664, 3.18415, 1.26038, 1.577, 0.443918, 3.09931, 1.86569, 2.45033, 1.07266, 
						1.74407, 4.2525, 2.99709, 4.74638, 4.53829, 0.404402, 1.63116, 4.08708, 6.01434, 5.60175, 2.1433, 3.22088, 5.46375, 5.19822, 4.5702, 4.64319, 
						4.08902, 6.16093, 3.87076, 4.36427, 4.37337, 2.90196};

					float3 newPoint = v.vertex.xyz + 1.5 * v.normal;

					for(int i = 0; i < 36; i++){
						float3 randomPoint = float3((2*sin(inclinations[i])*cos(azimuth[i])),(2*sin(inclinations[i])*sin(azimuth[i])),(2*cos(inclinations[i])));
						if((abs(v.vertex.x - randomPoint.x) < 0.03) && (abs(v.vertex.y - randomPoint.y) < 0.03) && (abs(v.vertex.z - randomPoint.z) < 0.03)){
							v.vertex.xyz = newPoint.xyz;
						}
						if((abs(v.vertex.x - randomPoint.x) < 0.01) && (abs(v.vertex.y - randomPoint.y) < 0.01)){
							v.vertex.xy = newPoint.xy;
						}
						if((abs(v.vertex.y - randomPoint.y) < 0.005) && (abs(v.vertex.z - randomPoint.z) < 0.01)){
							v.vertex.yz = newPoint.yz;
						}
						if((abs(v.vertex.x - randomPoint.x) < 0.01) && (abs(v.vertex.z - randomPoint.z) < 0.01)){
							v.vertex.xz = newPoint.xz;
						}
					}
				
			}

	   // Surface shader function
	   
	   void surf(Input IN, inout SurfaceOutput o) {
		   o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
	   }

	   ENDCG
	}
	Fallback "Diffuse"
}