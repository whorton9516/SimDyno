import React from 'react';
import { TelemetryDisplay } from './utility/TelemetryDisplay';
import { SignalRStatusBar } from './utility/SignalRStatusBar';
import { SignalRMessageLog } from './utility/SignalRMessageLog';
import '../../styles/ui/utility/_utility-body.scss';

const App: React.FC = () => {
  return (
    <div className = 'utility-body'>
      <h1 className = 'h1'>SimDynoUI</h1>
        <SignalRStatusBar />
        <TelemetryDisplay />
        <SignalRMessageLog />
    </div>
  );
};

export default App;