import React from 'react';
import { TelemetryDisplay } from './utility/TelemetryDisplay';
import { SignalRStatusBar } from './utility/SignalRStatusBar';
import { SignalRMessageLog } from './utility/SignalRMessageLog';
import { Speedometer } from './components/telemetry/speedometer';
import '../../styles/ui/utility/_utility-body.scss';

const App: React.FC = () => {
  return (
    <div className = 'utility-body'>
      <Speedometer />
      <SignalRStatusBar />
      <TelemetryDisplay />
      <SignalRMessageLog />
    </div>
  );
};

export default App;