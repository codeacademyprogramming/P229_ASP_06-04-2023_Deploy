fetch("https://localhost:44385/api/v2/categories", {
    method: "GET",
    mode:"'Access-Control-Allow-Origin",
    headers: {
      "Content-Type": "application/json",
      "Authorization":"Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiaGFtaWR2bSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImhhbWlkdm1AY29kZS5lZHUuYXoiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImVhNTAwMDY5LTI4MDktNDg0ZC1hMWJjLTJjOWI0ZWI5M2YzMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1lbWJlciIsImV4cCI6MTY4MDc5MTE1NH0.KFA2bpweZgOFG_022ONExy5q-os_oPZFZOYl2KPAHuCuK53mC9l-zGrSi41MRFrALZda0eCyvfuDTfnR06h5NA"
      // 'Content-Type': 'application/x-www-form-urlencoded',
    }}).then(res=>res.json())
.then(data=>{
    console.log(data);
})