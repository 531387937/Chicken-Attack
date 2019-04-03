﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace DragonBone
{
	/// <summary>
	/// parse animation file
	/// author:  bingheliefeng
	/// </summary>
	public class JsonParse : MonoBehaviour {

		public static void ParseTextureAtlas(ArmatureEditor armatureEditor , Texture2D texture, TextAsset atlasText )
		{
			SimpleJSON.JSONClass obj = SimpleJSON.JSON.Parse(atlasText.text).AsObject;
			SimpleJSON.JSONArray arr = obj["SubTexture"].AsArray;
			for(int i=0;i<arr.Count;++i){
				SimpleJSON.JSONClass frameObj = arr[i].AsObject;
				string textureName = frameObj["name"].ToString();
				textureName = textureName.Replace('/','_');

				ArmatureEditor.Atlas atlas =new ArmatureEditor.Atlas();
				atlas.texture = texture;
				atlas.atlasText = atlasText;
				armatureEditor.atlasKV[textureName] = atlas;
			}
		}


		public static void ParseAnimJsonData(ArmatureEditor armatureEditor)
		{
			SimpleJSON.JSONClass json=SimpleJSON.JSON.Parse(armatureEditor.animTextAsset.text.Replace("null","\"null\"")).AsObject;
			SimpleJSON.JSONArray armtureArr = json["armature"].AsArray;
			for(int i=0;i<armtureArr.Count;++i)
			{
				armatureEditor.armatureData = new DragonBoneData.ArmatureData();
				GameObject go = new GameObject("DragonBone");
				go.AddComponent<DragonBoneArmature>();
				armatureEditor.armature = go.transform;
				armatureEditor.bonesKV.Clear();
				armatureEditor.slotsKV.Clear();
				armatureEditor.slots.Clear();
				armatureEditor.bonesDataKV.Clear();
				armatureEditor.slotsDataKV.Clear();
				armatureEditor.bones.Clear();
				armatureEditor.ffdKV.Clear();
				armatureEditor.bonePoseKV.Clear();

				SimpleJSON.JSONClass armtureObj = armtureArr[i].AsObject;
				if(armtureObj.ContainKey("name")){
					string armatureName = armtureObj["name"].ToString().Trim();
					armatureEditor.armature.name = armatureName;
				}
				if(armtureObj.ContainKey("type")){
					armatureEditor.armatureData.type = armtureObj["type"].ToString();
				}
				if(armtureObj.ContainKey("frameRate")){
					armatureEditor.armatureData.frameRate = armtureObj["frameRate"].AsFloat;
					if(armatureEditor.armatureData.frameRate==0) armatureEditor.armatureData.frameRate = 24;//db默认为24
				}
				ParseArmtureData(armatureEditor,armtureObj);
				armatureEditor.InitShow();
			}

		}


		public static void ParseArmtureData(ArmatureEditor armatureEditor, SimpleJSON.JSONClass armtureObj ){
			//parse bone data
			if(armtureObj.ContainKey("bone")){
				SimpleJSON.JSONArray bones = armtureObj["bone"].AsArray;
				DragonBoneData.BoneData[] boneDatas = new DragonBoneData.BoneData[bones.Count];
				for(int i=0;i<bones.Count;++i){
					SimpleJSON.JSONClass boneObj = bones[i].AsObject;
					DragonBoneData.BoneData boneData = new DragonBoneData.BoneData();
					if(boneObj.ContainKey("length"))  boneData.length = boneObj["length"].AsFloat;
					if(boneObj.ContainKey("name"))  boneData.name = boneObj["name"].ToString().Trim();
					if(boneObj.ContainKey("parent"))  boneData.parent = boneObj["parent"].ToString();
					if(boneObj.ContainKey("inheritRotation")) boneData.inheritRotation = boneObj["inheritRotation"].AsInt==1?true:false;
					if(boneObj.ContainKey("inheritScale")) boneData.inheritScale = boneObj["inheritScale"].AsInt==1?true:false;
					if(boneObj.ContainKey("transform")){
						SimpleJSON.JSONClass transformObj = boneObj["transform"].AsObject;
						DragonBoneData.TransformData transData = new DragonBoneData.TransformData();
						if(transformObj.ContainKey("x")) transData.x = transformObj["x"].AsFloat*0.01f;
						if(transformObj.ContainKey("y")) transData.y = -transformObj["y"].AsFloat*0.01f;
						if(transformObj.ContainKey("skX")) transData.rotate = -transformObj["skX"].AsFloat;
						if(transformObj.ContainKey("scX")) transData.scx = transformObj["scX"].AsFloat;
						if(transformObj.ContainKey("scY")) transData.scy = transformObj["scY"].AsFloat;
						boneData.transform = transData;
					}
					boneDatas[i] = boneData;
					armatureEditor.bonesDataKV[boneData.name]=boneData;
				}
				armatureEditor.armatureData.boneDatas = boneDatas;
			}

			//parse slot data
			if(armtureObj.ContainKey("slot")){
				SimpleJSON.JSONArray slots = armtureObj["slot"].AsArray;
				DragonBoneData.SlotData[] slotDatas = new DragonBoneData.SlotData[slots.Count];
				bool isMC = armatureEditor.armatureData.type.Equals("MovieClip");
				for(int i=0;i<slots.Count;++i){
					SimpleJSON.JSONClass slotObj = slots[i].AsObject;
					DragonBoneData.SlotData slotData=new DragonBoneData.SlotData();
					if(slotObj.ContainKey("name"))  slotData.name = slotObj["name"].ToString();
					if(slotObj.ContainKey("parent"))  slotData.parent = slotObj["parent"].ToString();
					if(slotObj.ContainKey("z"))  slotData.z = -slotObj["z"].AsFloat*armatureEditor.zoffset;
					if(!isMC){
						if(slotObj.ContainKey("displayIndex")) slotData.displayIndex = slotObj["displayIndex"].AsInt;
					}
					if(slotObj.ContainKey("scale")) slotData.scale = slotObj["scale"].AsFloat;
					if(slotObj.ContainKey("blendMode")) slotData.blendMode = slotObj["blendMode"].ToString();
					if(slotObj.ContainKey("color"))
					{
						SimpleJSON.JSONClass colorObj = slotObj["color"].AsObject;
						DragonBoneData.ColorData colorData = new DragonBoneData.ColorData();
						if(colorObj.ContainKey("aM")) {
							colorData.aM = colorObj["aM"].AsFloat*0.01f;
						}
						if(colorObj.ContainKey("a0")){
							colorData.aM+=colorObj["a0"].AsFloat/255f;
						}
						if(colorObj.ContainKey("rM")) {
							colorData.rM = colorObj["rM"].AsFloat*0.01f;
						}
						if(colorObj.ContainKey("r0")){
							colorData.rM+=colorObj["r0"].AsFloat/255f;
						}
						if(colorObj.ContainKey("gM")) {
							colorData.gM = colorObj["gM"].AsFloat*0.01f;
						}
						if(colorObj.ContainKey("g0")){
							colorData.gM+=colorObj["g0"].AsFloat/255f;
						}
						if(colorObj.ContainKey("bM")) {
							colorData.bM = colorObj["bM"].AsFloat*0.01f;
						}
						if(colorObj.ContainKey("b0")){
							colorData.bM+=colorObj["b0"].AsFloat/255f;
						}
						slotData.color = colorData;
					}

					slotDatas[i] = slotData;
					armatureEditor.slotsDataKV[slotData.name]=slotData;
				}
				armatureEditor.armatureData.slotDatas = slotDatas;
			}

			//parse IK data
			if(armtureObj.ContainKey("ik"))
			{
				SimpleJSON.JSONArray iks = armtureObj["ik"].AsArray;
				DragonBoneData.IKData[] ikDatas = new DragonBoneData.IKData[iks.Count];
				for(int i=0;i<iks.Count;++i)
				{
					SimpleJSON.JSONClass ikObj = iks[i].AsObject;
					DragonBoneData.IKData ikData = new DragonBoneData.IKData();
					if(ikObj.ContainKey("name")) ikData.name = ikObj["name"].ToString();
					if(ikObj.ContainKey("bone")) ikData.bone = ikObj["bone"].ToString();
					if(ikObj.ContainKey("target")) ikData.target = ikObj["target"].ToString();
					if(ikObj.ContainKey("bendPositive")) ikData.bendPositive = ikObj["bendPositive"].AsBool;
					if(ikObj.ContainKey("chain")) ikData.chain = ikObj["chain"].AsInt;
					if(ikObj.ContainKey("weight")) ikData.weight = ikObj["weight"].AsFloat;
					ikDatas[i] = ikData;
				}
				armatureEditor.armatureData.ikDatas = ikDatas;
			}

			//parse animation file
			if(armtureObj.ContainKey("animation")){
				SimpleJSON.JSONArray anims = armtureObj["animation"].AsArray;
				DragonBoneData.AnimationData[] animDatas = new DragonBoneData.AnimationData[anims.Count];
				for(int i=0;i<anims.Count;++i){
					SimpleJSON.JSONClass animObj = anims[i].AsObject;
					DragonBoneData.AnimationData animData=new DragonBoneData.AnimationData();
					if(animObj.ContainKey("name"))  animData.name = animObj["name"].ToString().Trim();
					if(animObj.ContainKey("playTimes"))  animData.playTimes = animObj["playTimes"].AsInt;
					if(animObj.ContainKey("duration"))  animData.duration = animObj["duration"].AsInt;
					if(animData.duration==0) animData.duration =1;
					if(animObj.ContainKey("frame")) {
						ParseAnimFrames(animObj["frame"].AsArray,animData);
					}
					if(animObj.ContainKey("bone")){
						SimpleJSON.JSONArray bones = animObj["bone"].AsArray;
						animData.boneDatas = new DragonBoneData.AnimSubData[bones.Count];
						ParsetAnimBoneSlot(armatureEditor, bones , animData.boneDatas );
					}
					if(animObj.ContainKey("slot")){
						SimpleJSON.JSONArray slots = animObj["slot"].AsArray;
						animData.slotDatas = new DragonBoneData.AnimSubData[slots.Count];
						ParsetAnimBoneSlot(armatureEditor, slots , animData.slotDatas );
					}
					//ffd
					if(animObj.ContainKey("ffd")){
						SimpleJSON.JSONArray ffds = animObj["ffd"].AsArray;
						animData.ffdDatas = new DragonBoneData.AnimSubData[ffds.Count];
						ParsetAnimBoneSlot(armatureEditor, ffds , animData.ffdDatas );
					}
					//zOrder
					if(animObj.ContainKey("zOrder")){
						SimpleJSON.JSONClass zOrders = animObj["zOrder"].AsObject;
						ParseAnimSortOrder(armatureEditor, zOrders , animData );
					}
					animDatas[i] = animData;
				}
				armatureEditor.armatureData.animDatas = animDatas;
			}

			//parse skin data
			if(armtureObj.ContainKey("skin")){
				SimpleJSON.JSONArray skins = armtureObj["skin"].AsArray;
				DragonBoneData.SkinData[] skinDatas = new DragonBoneData.SkinData[skins.Count];
				for(int i=0;i<skins.Count;++i){
					DragonBoneData.SkinData skinData = new DragonBoneData.SkinData();
					skinDatas[i] = skinData;
					SimpleJSON.JSONClass skinObj = skins[i].AsObject;
					string skinName = skinObj["name"].ToString();
					skinData.skinName = skinName;
					if(skinObj.ContainKey("slot"))
					{
						SimpleJSON.JSONArray slots = skinObj["slot"].AsArray;
						skinData.slots = new DragonBoneData.SkinSlotData[slots.Count];
						for(int j=0;j<slots.Count;++j){
							DragonBoneData.SkinSlotData skinSlotData = new DragonBoneData.SkinSlotData();
							SimpleJSON.JSONClass slot = slots[j].AsObject;
							skinData.slots[j] = skinSlotData;
							if(slot.ContainKey("name")){
								skinSlotData.slotName = slot["name"].ToString();
							}
							if(slot.ContainKey("display")){
								SimpleJSON.JSONArray display = slot["display"].AsArray;
								skinSlotData.displays = new DragonBoneData.SkinSlotDisplayData[display.Count];
								for(int k = 0 ;k<display.Count;++k){
									DragonBoneData.SkinSlotDisplayData displayData= new DragonBoneData.SkinSlotDisplayData();
									skinSlotData.displays[k] = displayData;
									SimpleJSON.JSONClass displayObj = display[k].AsObject;
									if(displayObj.ContainKey("name")) displayData.textureName = displayObj["name"].ToString().Replace('/','_');
									if(displayObj.ContainKey("path")) displayData.texturePath = displayObj["path"].ToString();
									else displayData.texturePath = displayData.textureName;
									if(displayObj.ContainKey("type")) displayData.type = displayObj["type"].ToString();
									if(displayObj.ContainKey("subType")) displayData.subType = displayObj["subType"].ToString();
									if(displayObj.ContainKey("pivot")) {
										displayData.pivot = new Vector2(displayObj["pivot"].AsObject["x"].AsFloat,displayObj["pivot"].AsObject["y"].AsFloat);
									}
									if(displayObj.ContainKey("transform")){
										SimpleJSON.JSONClass transformObj = displayObj["transform"].AsObject;
										DragonBoneData.TransformData transData = new DragonBoneData.TransformData();
										if(transformObj.ContainKey("x")) transData.x = transformObj["x"].AsFloat*0.01f;
										if(transformObj.ContainKey("y")) transData.y = -transformObj["y"].AsFloat*0.01f;
										if(transformObj.ContainKey("skX")) transData.rotate = -transformObj["skX"].AsFloat;
										if(transformObj.ContainKey("scX")) transData.scx = transformObj["scX"].AsFloat;
										if(transformObj.ContainKey("scY")) transData.scy = transformObj["scY"].AsFloat;
										displayData.transform = transData;
									}
									//uv
									if(displayObj.ContainKey("uvs")){
										SimpleJSON.JSONArray uvsObj = displayObj["uvs"].AsArray;
										int index = 0;
										displayData.uvs=new Vector2[uvsObj.Count/2];
										for(int z =0;z<uvsObj.Count;z+=2){
											Vector2 uv = new Vector2(uvsObj[z].AsFloat,1-uvsObj[z+1].AsFloat);
											displayData.uvs[index] = uv;
											++index;
										}
									}


									//weight
									if(displayObj.ContainKey("weights")){
										SimpleJSON.JSONArray weightsObj = displayObj["weights"].AsArray;
										displayData.weights=new float[weightsObj.Count];
										for(int z =0;z<weightsObj.Count;++z){
											displayData.weights[z] = weightsObj[z].AsFloat;
										}
									}
									//bonepose
									if(displayObj.ContainKey("bonePose")){
										SimpleJSON.JSONArray bonePoseObj = displayObj["bonePose"].AsArray;
										displayData.bonePose = new float[bonePoseObj.Count];
										for(int z=0;z<bonePoseObj.Count;z+=7){
											displayData.bonePose[z] = bonePoseObj[z].AsFloat;
											displayData.bonePose[z+1] = bonePoseObj[z+1].AsFloat;//a
											displayData.bonePose[z+2] = bonePoseObj[z+2].AsFloat;//b
											displayData.bonePose[z+3] = bonePoseObj[z+3].AsFloat;//c
											displayData.bonePose[z+4] = bonePoseObj[z+4].AsFloat;//d
											displayData.bonePose[z+5] = bonePoseObj[z+5].AsFloat;//tx
											displayData.bonePose[z+6] = bonePoseObj[z+6].AsFloat;//ty

											Matrix2D m = new Matrix2D(displayData.bonePose[z+1],displayData.bonePose[z+2],
												displayData.bonePose[z+3],displayData.bonePose[z+4],displayData.bonePose[z+5],displayData.bonePose[z+6]);
											armatureEditor.bonePoseKV[displayData.textureName + armatureEditor.armatureData.boneDatas[(int)displayData.bonePose[z]].name ] = m;
										}
									}

									Matrix2D slotPoseMat = null;
									//slotpose
									if(displayObj.ContainKey("slotPose")){
										SimpleJSON.JSONArray slotPoseObj = displayObj["slotPose"].AsArray;
										slotPoseMat = new Matrix2D(slotPoseObj[0].AsFloat,slotPoseObj[1].AsFloat,slotPoseObj[2].AsFloat,
											slotPoseObj[3].AsFloat,slotPoseObj[4].AsFloat,slotPoseObj[5].AsFloat);
									}

									//vertex
									if(displayObj.ContainKey("vertices")){
										SimpleJSON.JSONArray verticesObj = displayObj["vertices"].AsArray;
										displayData.vertices=new Vector3[verticesObj.Count/2];

										for(int z =0;z<verticesObj.Count;z+=2){
											int vertexIndex = z / 2;
											Vector3 vertex = new Vector3(verticesObj[z].AsFloat,verticesObj[z+1].AsFloat,0f);
											if(slotPoseMat!=null){
												//slotPose转换
												vertex = (Vector3)slotPoseMat.TransformPoint(vertex.x,vertex.y);
											}
											vertex.x*=0.01f;
											vertex.y*=-0.01f;
											displayData.vertices[vertexIndex] = vertex;
										}
									}
									//triangles
									if(displayObj.ContainKey("triangles")){
										SimpleJSON.JSONArray trianglesObj = displayObj["triangles"].AsArray;
										displayData.triangles=new int[trianglesObj.Count];
										for(int z =0;z<trianglesObj.Count;z++){
											displayData.triangles[z] = trianglesObj[z].AsInt;
										}
										//dragonBone和unity的z相反
										for(int z =0;z<displayData.triangles.Length;z+=3){
											int f1 = displayData.triangles[z];
											int f3 = displayData.triangles[z+2];
											displayData.triangles[z] = f3;
											displayData.triangles[z+2] = f1;
										}
									}
									//edges 

									//userdeges
								}
							}
						}
					}
				}
				armatureEditor.armatureData.skinDatas = skinDatas;
			}
		}

		private static void ParseAnimSortOrder(ArmatureEditor armatureEditor, SimpleJSON.JSONClass zOrders  ,DragonBoneData.AnimationData animData){
			if(zOrders.ContainKey("frame")){
				//just only one
				DragonBoneData.AnimSubData subData = new DragonBoneData.AnimSubData();
				animData.zOrderDatas = new DragonBoneData.AnimSubData[1]{subData};
				if(zOrders.ContainKey("offset")) subData.offset = zOrders["offset"].AsFloat;

				SimpleJSON.JSONArray frames = zOrders["frame"].AsArray;
				subData.frameDatas = new DragonBoneData.AnimFrameData[frames.Count];
				for(int i=0;i<frames.Count;++i){
					SimpleJSON.JSONClass frameObj = frames[i].AsObject;
					DragonBoneData.AnimFrameData frameData = new DragonBoneData.AnimFrameData();
					subData.frameDatas[i] = frameData;

					if(frameObj.ContainKey("duration")) frameData.duration = frameObj["duration"].AsInt;
					if(frameObj.ContainKey("zOrder")){
						SimpleJSON.JSONArray zs = frameObj["zOrder"].AsArray;
						if(zs!=null){
							frameData.zOrder = new int[zs.Count];
							for(int z=0;z<zs.Count;++z){
								frameData.zOrder[z] = zs[z].AsInt;
							}
						}
					}
				}
			}
		}

		public static void ParseAnimFrames( SimpleJSON.JSONArray animFrames ,DragonBoneData.AnimationData animData){
			animData.keyDatas = new DragonBoneData.AnimKeyData[animFrames.Count];
			for(int i=0;i<animFrames.Count;++i){
				SimpleJSON.JSONClass frameObj = animFrames[i].AsObject;
				DragonBoneData.AnimKeyData keyData = new DragonBoneData.AnimKeyData();
				if(frameObj.ContainKey("event")) keyData.eventName = frameObj["event"].ToString();
				if(frameObj.ContainKey("sound")) keyData.soundName = frameObj["sound"].ToString();
				if(frameObj.ContainKey("duration")) keyData.duration = frameObj["duration"].AsInt;
				if(keyData.duration==0) keyData.duration=1;
				if(frameObj.ContainKey("action")) keyData.actionName = frameObj["action"].ToString();
				animData.keyDatas[i] = keyData;
			}
		}
		public static void ParsetAnimBoneSlot(ArmatureEditor armatureEditor, SimpleJSON.JSONArray animBonesSlots , DragonBoneData.AnimSubData[] animDatas){
			for(int i=0;i<animBonesSlots.Count;++i){
				SimpleJSON.JSONClass boneSlotObj = animBonesSlots[i].AsObject;
				DragonBoneData.AnimSubData subData = new DragonBoneData.AnimSubData();
				if(boneSlotObj.ContainKey("name")) subData.name = boneSlotObj["name"].ToString().Replace('/','_');
				if(boneSlotObj.ContainKey("slot")) subData.slot = boneSlotObj["slot"].ToString().Replace('/','_');
				if(boneSlotObj.ContainKey("scale")) subData.scale = boneSlotObj["scale"].AsFloat;
				if(boneSlotObj.ContainKey("offset")) subData.offset = boneSlotObj["offset"].AsFloat;
				if(boneSlotObj.ContainKey("frame")){ //for 5.3 以下
					SimpleJSON.JSONArray frames = boneSlotObj["frame"].AsArray;
					if(frames != null && frames.Count>0) {
						subData.frameDatas = new DragonBoneData.AnimFrameData[frames.Count];
						ParseFrames(armatureEditor,subData.frameDatas,frames,subData.name,DragonBoneData.FrameType.Frame);
					}
				}
				else //for 5.5 及以上
				{
					if(boneSlotObj.ContainKey("translateFrame")){
						SimpleJSON.JSONArray frames = boneSlotObj["translateFrame"].AsArray;
						if(frames != null && frames.Count>0) {
							subData.translateFrameDatas = new DragonBoneData.AnimFrameData[frames.Count];
							ParseFrames(armatureEditor,subData.translateFrameDatas,frames,subData.name,DragonBoneData.FrameType.TranslateFrame);
						}
					}
					if(boneSlotObj.ContainKey("rotateFrame")){
						SimpleJSON.JSONArray frames = boneSlotObj["rotateFrame"].AsArray;
						if(frames != null && frames.Count>0) {
							subData.rotateFrameDatas = new DragonBoneData.AnimFrameData[frames.Count];
							ParseFrames(armatureEditor,subData.rotateFrameDatas,frames,subData.name,DragonBoneData.FrameType.RotateFrame);
						}
					}
					if(boneSlotObj.ContainKey("scaleFrame")){
						SimpleJSON.JSONArray frames = boneSlotObj["scaleFrame"].AsArray;
						if(frames != null && frames.Count>0) {
							subData.scaleFrameDatas = new DragonBoneData.AnimFrameData[frames.Count];
							ParseFrames(armatureEditor,subData.scaleFrameDatas,frames,subData.name,DragonBoneData.FrameType.ScaleFrame);
						}
					}

					if(boneSlotObj.ContainKey("colorFrame")){
						SimpleJSON.JSONArray frames = boneSlotObj["colorFrame"].AsArray;
						if(frames != null && frames.Count>0) {
							subData.colorFrameDatas = new DragonBoneData.AnimFrameData[frames.Count];
							ParseFrames(armatureEditor,subData.colorFrameDatas,frames,subData.name,DragonBoneData.FrameType.ColorFrame);
						}
					}

					if(boneSlotObj.ContainKey("displayFrame")){
						SimpleJSON.JSONArray frames = boneSlotObj["displayFrame"].AsArray;
						if(frames != null && frames.Count>0) {
							subData.displayFrameDatas = new DragonBoneData.AnimFrameData[frames.Count];
							ParseFrames(armatureEditor,subData.displayFrameDatas,frames,subData.name,DragonBoneData.FrameType.DisplayFrame);
						}
					}
				}
				animDatas[i] = subData;
			}

		}

		static void ParseFrames(ArmatureEditor armatureEditor,DragonBoneData.AnimFrameData[] frameDatas, SimpleJSON.JSONArray frames,string subDataName,DragonBoneData.FrameType type){
			for(int j=0;j<frames.Count;++j){
				SimpleJSON.JSONClass frameObj = frames[j].AsObject;
				DragonBoneData.AnimFrameData frameData=new DragonBoneData.AnimFrameData();
				if(frameObj.ContainKey("duration")) frameData.duration = frameObj["duration"].AsInt;
				if(frameData.duration==0) frameData.duration=1;
				if(frameObj.ContainKey("displayIndex")) frameData.displayIndex = frameObj["displayIndex"].AsInt;
				if (type == DragonBoneData.FrameType.DisplayFrame) {
					if (frameObj.ContainKey ("value")) {
						frameData.displayIndex = frameObj ["value"].AsInt;
					}
				}
				if(frameObj.ContainKey("z")) frameData.z = -frameObj["z"].AsInt*armatureEditor.zoffset;
				if(frameObj.ContainKey("tweenEasing") && frameObj["tweenEasing"].ToString()!="null") frameData.tweenEasing = frameObj["tweenEasing"].AsFloat;
				if(frameObj.ContainKey("tweenRotate")) frameData.tweenRotate = frameObj["tweenRotate"].AsInt;
				if(frameObj.ContainKey("curve")){
					SimpleJSON.JSONArray curves = frameObj["curve"].AsArray;
					if(curves.Count>3){
						frameData.curve = new float[4]{
							curves[0].AsFloat,
							curves[1].AsFloat,
							curves[curves.Count-2].AsFloat,
							curves[curves.Count-1].AsFloat
						};
					}
				}
				if(frameObj.ContainKey("transform")){
					SimpleJSON.JSONClass transformObj = frameObj["transform"].AsObject;
					DragonBoneData.TransformData transData = new DragonBoneData.TransformData();
					if(transformObj.ContainKey("x")) {
						transData.x = transformObj["x"].AsFloat*0.01f;
					}
					if(transformObj.ContainKey("y")) {
						transData.y = -transformObj["y"].AsFloat*0.01f;
					}
					if(transformObj.ContainKey("skX")) {
						transData.rotate = -transformObj["skX"].AsFloat;
					}
					if(transformObj.ContainKey("scX")) {
						transData.scx = transformObj["scX"].AsFloat;
					}
					if(transformObj.ContainKey("scY")){
						transData.scy = transformObj["scY"].AsFloat;
					}
					frameData.transformData = transData;
				}
				else
				{
					if(frameObj.ContainKey("x")){
						if(frameData.transformData==null)
							frameData.transformData = new DragonBoneData.TransformData();
						if(type==DragonBoneData.FrameType.TranslateFrame){
							frameData.transformData.x = frameObj["x"].AsFloat*0.01f;
						}else{
							//scx
							frameData.transformData.scx = frameObj["x"].AsFloat;
						}
					}
					if(frameObj.ContainKey("y")){
						if(frameData.transformData==null)
							frameData.transformData = new DragonBoneData.TransformData();

						if(type==DragonBoneData.FrameType.TranslateFrame){
							frameData.transformData.y = -frameObj["y"].AsFloat*0.01f;
						}else{
							//scy
							frameData.transformData.scy = frameObj["y"].AsFloat;
						}
					}
					if(frameObj.ContainKey("rotate")){
						if(frameData.transformData==null)
							frameData.transformData = new DragonBoneData.TransformData();
						frameData.transformData.rotate = -frameObj["rotate"].AsFloat;
					}
				}
				if(frameObj.ContainKey("color"))
				{
					SimpleJSON.JSONClass colorObj = frameObj["color"].AsObject;
					DragonBoneData.ColorData colorData = new DragonBoneData.ColorData();
					if(colorObj.ContainKey("aM")) {
						colorData.aM = colorObj["aM"].AsFloat*0.01f;
					}
					if(colorObj.ContainKey("a0")){
						colorData.aM+=colorObj["a0"].AsFloat/255f;
					}
					if(colorObj.ContainKey("rM")) {
						colorData.rM = colorObj["rM"].AsFloat*0.01f;
					}
					if(colorObj.ContainKey("r0")){
						colorData.rM+=colorObj["r0"].AsFloat/255f;
					}
					if(colorObj.ContainKey("gM")) {
						colorData.gM = colorObj["gM"].AsFloat*0.01f;
					}
					if(colorObj.ContainKey("g0")){
						colorData.gM+=colorObj["g0"].AsFloat/255f;
					}
					if(colorObj.ContainKey("bM")) {
						colorData.bM = colorObj["bM"].AsFloat*0.01f;
					}
					if(colorObj.ContainKey("b0")){
						colorData.bM+=colorObj["b0"].AsFloat/255f;
					}
					frameData.color = colorData;
				}

				//ffd animation
				//vertex offset
				bool startFromY = false;
				if(frameObj.ContainKey("offset")){
					startFromY = frameObj["offset"].AsInt%2!=0;//从Y开始
					frameData.offset = frameObj["offset"].AsInt/2;
				}
				if(frameObj.ContainKey("vertices")){ //local vertex
					SimpleJSON.JSONArray verticesObj = frameObj["vertices"].AsArray;
					int index=0;
					int k= 0;
					if(startFromY) {
						frameData.vertices = new Vector2[verticesObj.Count/2+1];
						frameData.vertices[index]=new Vector2(0,-verticesObj[k].AsFloat*0.01f);
						k = 1;
						++index;
					}else{
						frameData.vertices = new Vector2[verticesObj.Count/2];
					}
					for(;k<verticesObj.Count && k+1<verticesObj.Count;k+=2)
					{
						frameData.vertices[index]=new Vector2(verticesObj[k].AsFloat*0.01f,-verticesObj[k+1].AsFloat*0.01f);
						++index;
					}
					armatureEditor.ffdKV[subDataName] = true;
				}
				frameDatas[j] = frameData;
			}
		}
	}

}