import React from 'react';
import { Speedometer } from './components/telemetry/Speedometer';
import { SignalRStatusBar } from './utility/SignalRStatusBar';
import { TelemetryDisplay } from './utility/TelemetryDisplay';
import { SignalRMessageLog } from './utility/SignalRMessageLog';
import Tachometer from './components/telemetry/Tachometer';

// TODO: I need to rollback the UI changes for now. Remove the styles folder and place the styles directly in the components. Until I can set it back up.
// Just place the components directly into <App>.
// Use Austin Baccus's project as reference for now. Formatting and styling can happen later!
// Need to go back through and add comments for documentation.

const AppStyle: React.CSSProperties = {
  height: '100%',
  width: '100%',
};

const App: React.FC = () => {
  return (
    <div style={AppStyle}>
      <Speedometer />
      <Tachometer />
      <SignalRStatusBar />
      <TelemetryDisplay />
      <SignalRMessageLog />
    </div>
  );
};

export default App;