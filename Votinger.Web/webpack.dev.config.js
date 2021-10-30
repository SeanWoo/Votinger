const baseConfig = require('./webpack.base.config');

module.exports = {
    ...baseConfig,
    mode: 'development',
    devtool: 'inline-source-map',
    watch: true
};