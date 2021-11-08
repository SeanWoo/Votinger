const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin')
const HtmlWebpackPlugin = require('html-webpack-plugin');

const buildDir = './build/dist';

module.exports = {
    entry: path.resolve(__dirname, './src/index.tsx'),
    output: {
        filename: '[name].[contenthash:8].js',
        globalObject: 'this',
        path: path.resolve(__dirname, buildDir),
        publicPath: '/'
    },
    resolve: {
        modules: ['node_modules'],
        extensions: ['.ts', '.tsx', '.js', '.jsx']
    },
    module: {
        rules: [{
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            },
            {
                test: /\.s[ac]ss$/i,
                use: [
                    "style-loader",
                    "css-loader",
                    {
                        loader: "sass-loader",
                        options: {
                            // Prefer `dart-sass`
                            implementation: require("sass"),
                        },
                    },
                ],
            },
        ]
    },
    stats: {
        colors: true
    },
    devServer: {
        static: {
            directory: path.resolve(__dirname, './src/public'),
        },
        compress: true,
        port: 3000,
        historyApiFallback: true,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
            "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization"
        }
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, './src/public/index.html')
        }),
        new CleanWebpackPlugin()
    ]
};