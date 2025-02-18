
import { Button, Text, Group } from '@mantine/core';
import { Minus, Square, X } from "lucide-react";

export function TitleBar() {

  return (
    <div style={{
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
      padding: '0 16px',
      height: '32px',
      backgroundColor: 'var(--mantine-color-body)',
      borderBottom: '1px solid var(--mantine-color-gray-3)',
    }}>
      <Text>SimDynoUI</Text>
      
      <Group style={{ WebkitAppRegion: 'no-drag' }}>
        <Button variant="subtle" onClick={() => window.electron.minimize}>
          <Minus size={16} />
        </Button>
        <Button variant="subtle" onClick={() => window.electron.maximize}>
          <Square size={16} />
        </Button>
        <Button variant="subtle" color="red" onClick={() => window.electron.close}>
          <X size={16} />
        </Button>
      </Group>
    </div>
  );
}