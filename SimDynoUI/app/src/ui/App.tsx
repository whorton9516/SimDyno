import React from 'react';
import { TelemetryDisplay } from './utility/TelemetryDisplay';
import { SignalRStatusBar } from './utility/SignalRStatusBar';
import { SignalRMessageLog } from './utility/SignalRMessageLog';
import { Container, Title } from '@mantine/core';
import '../../styles/ui/utility/_utility-body.scss';

const App: React.FC = () => {
  return (
    <Container style={{padding: '1rem' }}>
      <div className = 'utility-body'>
        <SignalRStatusBar />
        <TelemetryDisplay />
        <SignalRMessageLog />
      </div>
    </Container>
  );
};

export default App;