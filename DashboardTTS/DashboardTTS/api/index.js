// 这里是管理所有请求后台的 api
 import myAxios from './myAxios.js'


 // 2021-5-6, 获取 pqc bin his 中的 scrap 的数据
 export const GetPQCScrapList =  (url, data) => { myAxios.post(url, data); }