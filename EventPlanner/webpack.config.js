var webpack = require('webpack');

module.exports = {
    entry: {
        event: [
            './EventPlanner.Web/Scripts/Custom/Event/Page.js',
            './EventPlanner.Web/Scripts/Custom/Event/PageInit.js'
        ],
        vote: [
            './EventPlanner.Web/Scripts/Custom/Vote/GoogleMapsModule.js',
            './EventPlanner.Web/Scripts/Custom/Vote/PageInit.js'
        ]
    },
    output: {
        path: './EventPlanner.Web/Scripts/Compiled/',
        filename: '[name]-bundle.js'
    },
    module: {
        loaders: [{
            test: /\.js?$/,
            exclude: /node_modules/,
            loader: 'babel-loader'
        }]
    },
    devtool: 'source-map'
};
