import React from 'react';
import { Speedometer } from './components/telemetry/Speedometer/Speedometer';
import { SignalRStatusBar } from './utility/SignalRStatusBar';
import { TelemetryDisplay } from './utility/TelemetryDisplay';
import { SignalRMessageLog } from './utility/SignalRMessageLog';
import Tachometer from './components/telemetry/Tachometer/Tachometer';
import Powerband from './components/telemetry/Powerband/Powerband';
import MainLayout from './components/layout/MainLayout';

// TODO: I need to rollback the UI changes for now. Remove the styles folder and place the styles directly in the components. Until I can set it back up.
// Just place the components directly into <App>.
// Need to go back through and add comments for documentation.

const AppStyle: React.CSSProperties = {
  height: '100%',
  width: '100%',
};

const App: React.FC = () => {
  return (
    <div style={AppStyle}>
      <MainLayout>
        <Speedometer />
        <Tachometer />
        <Powerband />
        <SignalRStatusBar />
        <TelemetryDisplay />
        <SignalRMessageLog />
      </MainLayout>
    </div>
  );
};

export default App;