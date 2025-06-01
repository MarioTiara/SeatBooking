const path = require('path');

module.exports = {
  entry: './ClientApp/index.jsx',
  output: {
    path: path.resolve(__dirname, 'wwwroot/Scripts/bundle'), // <-- output to wwwroot
    filename: 'bundle.js'
  },
  resolve: {
    extensions: ['.js', '.jsx']
  },
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: 'babel-loader'
      },
      {
         test: /\.css$/i,
  use: ['style-loader', 'css-loader', 'postcss-loader'],
      }
    ]
  },
  mode: 'development'
};
