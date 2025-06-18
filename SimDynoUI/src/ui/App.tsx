import React from 'react';
import Canvas from './canvas';
import MainLayout from './components/layout/MainLayout';

const AppStyle: React.CSSProperties = {
  height: '100%',
  width: '100%',
};

const App: React.FC = () => {
  return (
    <div style={AppStyle}>
      <MainLayout>
        <Canvas />
      </MainLayout>
    </div>
  );
};

export default App;