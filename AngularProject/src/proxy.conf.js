const { env } = require('process');

//const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7001';

const PROXY_CONFIG = [
    {
        //target: "https://softwaregestionapi.azurewebsites.net",
        target: "https://localhost:7001",
        secure: false,
        context: [
            "/api/v1/**",
        ]
    }
]

module.exports = PROXY_CONFIG;
