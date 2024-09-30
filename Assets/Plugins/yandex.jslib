var sendSuccessfullResponse = (method, data) => {
	var response = {
		isSuccessfull: true,
		data: data,
		error: null,
		method: method
	};
	window.unityInstance.SendMessage('YandexApi', method, JSON.stringify(response));
};

var sendFailedResponse = (method, errorString) => {
	var response = {
		isSuccessfull: false,
		data: null,
		error: errorString
	};
	window.unityInstance.SendMessage('YandexApi', method, JSON.stringify(response));
};

mergeInto(LibraryManager.library, {
	Init: function(){
		let methodName = 'OnInitCallback';
		YaGames.init().then(ysdk => {
			console.log('Yandex SDK initialized');
			window.ysdk = ysdk;
			window.ysdk.onEvent(ysdk.EVENTS.HISTORY_BACK, () => {
				window.unityInstance.SendMessage('YandexApi', 'OnHistoryBackEvent');
			});
			sendSuccessfullResponse(methodName, null);
		})
		.catch(err => {
			sendFailedResponse(methodName, JSON.stringify(err));
		});
	},
	SetData: function(dataJson){			
		let methodName = 'OnSetDataCallback';
		if (!window.player) {
			sendFailedResponse(methodName, "Player is not initialised");
		};
		let data = JSON.parse(UTF8ToString(dataJson));
		window.player.setData(data).then(() => {
			sendSuccessfullResponse(methodName, null)
		}).catch(err => {
			sendFailedResponse(methodName, JSON.stringify(err));
		});
	},
	GetData: function(){
		let methodName = 'OnGetDataCallback';
		window.player.getData().then((data) => {
			sendSuccessfullResponse(methodName, data);
		}).catch(err => {
			sendFailedResponse(methodName, JSON.stringify(err));
		});
	},
	GetPlayer: function(){
		let methodName = 'OnGetPlayerCallback';
		window.ysdk.getPlayer({ scopes: true })
		.then(player => {
			window.player = player;
			sendSuccessfullResponse(methodName, null);
		})
		.catch(err => {
			sendFailedResponse(methodName, JSON.stringify(err));
		});
	},
	GetPlayerInfo: function(){
		let response = {
			isSuccessfull: true,
			data: null,
			error: null
		};
		try {
			let playerObject = {
				id: window.player.getUniqueID(),
				name: window.player.getName(),
				photoUrls: [
					window.player.getPhoto("small"),
					window.player.getPhoto("medium"),
					window.player.getPhoto("large")]
			};
			response.data = playerObject
		} catch (error) {
			response.isSuccessfull = false;
			response.error = JSON.stringify(error);
		} 
		finally{
			let dataJson = JSON.stringify(response);
			let bufferSize = lengthBytesUTF8(dataJson) + 1;
			let buffer = _malloc(bufferSize);
			stringToUTF8(dataJson, buffer, bufferSize);
			return buffer;
		}
	},
	ShowFullscreenAdv: function(){
		let methodName = 'OnFullscreenAdvCallback';
		window.ysdk.adv.showFullscreenAdv({callbacks:{
			onClose: function(wasShown) {
				console.log("OnCLose");
				if (wasShown) {					
					sendSuccessfullResponse(methodName, null);
					return;
				}
				sendFailedResponse(methodName, "Ad was not shown");			
			},
			onError: function(err) {
				console.log("OnError");				
				sendFailedResponse(methodName, JSON.stringify(err));
			}
		}});
	},
	ShowRewardedVideo: function(){
		let methodName = 'OnRewardedVideoCallback';
		window.ysdk.adv.showRewardedVideo({callbacks:{
			onOpen: () => {
				let data = {
					action: 3
				};
				console.log('onOpen');
				sendSuccessfullResponse(methodName, data);
			},
			onRewarded: () => {
				let data = {
					action: 1
				};
				console.log('onRewarded');
				sendSuccessfullResponse(methodName, data);	
			},
			onClose: function() {
				let data = {
					action: 2
				};
				console.log("OnCLose");
				sendSuccessfullResponse(methodName, data);	
			},
			onError: function(err) {
				console.log("OnError");				
				sendFailedResponse(methodName, JSON.stringify(err));
			}
		}});
	},
	SendExitEvent: function(){
		window.ysdk.dispatchEvent(ysdk.EVENTS.EXIT);
	},
	DeviceInfo: function(){
		switch (window.ysdk.deviceInfo.type){
			case "desktop":
				return 1;
			case "mobile":
				return 2;
			case "tablet":
				return 3;
			case "tv":
				return 4;
		}
		return 1;
	}
});