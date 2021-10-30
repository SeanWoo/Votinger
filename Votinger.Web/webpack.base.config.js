const path = require('path');
const { WebpackManifestPlugin } = require('webpack-manifest-plugin');
const WebpackOnBuildPlugin = require('on-build-webpack');
const fs = require('fs');

const buildDir = './wwwroot/dist/';

module.exports = {
    entry: './Content/expose-components.js',
    output: {
        filename: '[name].[contenthash:8].js',
        globalObject: 'this',
        path: path.resolve(__dirname, 'wwwroot/dist'),
        publicPath: '/dist/'
    },
    optimization: {
        runtimeChunk: {
            name: 'runtime' // necessary when using multiple entrypoints on the same page
        }
    },
    resolve: {
        extensions: ['.ts', '.tsx', '.js', '.jsx']
    },
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                loader: 'babel-loader'
            }
        ]
    },
    stats: {
        colors: true
    },
    plugins: [
        new WebpackManifestPlugin({
            fileName: 'asset-manifest.json',
            generate: (seed, files) => {
                const manifestFiles = files.reduce((manifest, file) => {
                    manifest[file.name] = file.path;
                    return manifest;
                }, seed);

                const entrypointFiles = files.filter(x => x.isInitial && !x.name.endsWith('.map')).map(x => x.path);

                return {
                    files: manifestFiles,
                    entrypoints: entrypointFiles
                };
            }
        }),
        new WebpackOnBuildPlugin(function (stats) {
            const newlyCreatedAssets = stats.compilation.assets;

            const unlinked = [];
            fs.readdir(path.resolve(buildDir), (err, files) => {
                files.forEach(file => {
                    if (!newlyCreatedAssets[file]) {
                        fs.unlinkSync(path.resolve(buildDir + file));
                        unlinked.push(file);
                    }
                });
                if (unlinked.length > 0) {
                    console.log('Removed old assets: ', unlinked);
                }
            });
        })
    ]
};
