import axios from '../plugins/axios/axios.min.js'

// 配置 axios 全局配置
// axios.defaults.baseURL = 'http://127.0.0.1/Dashboard';
// axios.defaults.baseURL = 'http://127.0.0.1/Dashboard_DEV';
axios.defaults.baseURL = 'http://localhost:50340';
axios.defaults.timeout = 10000;

// 简单封装下 axios 的 get, post 方法
let myAxios = {
    get: function(url, data){
        return new Promise(function(resolve, reject){
            axios.get(url,{ params: data })
            .then((res) => {
                console.log(res);
                resolve(res);
            })
            .catch((err) => {
                console.log(err);
                reject(err);
            })
        })
    },
    post: function(url, data){
        return new Promise(function(resolve, reject){
            axios.post(url,data)
            .then((res) => {
                console.log(res);
                resolve(res);
            })
            .catch((err) => {
                console.log(err);
                reject(err);
            })
        })
    }
};

export default myAxios;
