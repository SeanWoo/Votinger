const baseConfig = require('./webpack.base.config');

module.exports = {
    ...baseConfig,
    mode: 'development',
    devtool: 'source-map',
    watch: true
};