import React, { ReactNode } from 'react';
import { AppShell, Burger, Group, Skeleton, Button } from '@mantine/core';
import { Minus, Square, X } from "lucide-react";
import { useDisclosure } from '@mantine/hooks';

interface MainLayoutProps {
    children: ReactNode;
  }

export function MainLayout({ children }: MainLayoutProps) {
  const [opened, { toggle }] = useDisclosure();

  console.log("window.electron:", window.electron);

  return (
    <AppShell header={{ height: 60 }}
              navbar={{ width: 300, breakpoint: 'sm', collapsed: { mobile: !opened } }}
              padding="md">
      <AppShell.Header
        style={{
            borderBottom: '1px solid var(--mantine-colors-dark-N100)',
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            backgroundColor: "#1e1e1e",
            color: "white",
            userSelect: "none",
            "-webkit-app-region": "drag",
          }}>
        <Group h="100%" px="md">
          <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" color="white"/>
          {/* <MantineLogo size={30} /> */}
        </Group>
        <Group style={{ WebkitAppRegion: "no-drag" }}>
          <Button variant="subtle" color="gray" size="xs" onClick={() => window.electron.minimize()}>
            <Minus size={16} />
          </Button>
          <Button variant="subtle" color="gray" size="xs" onClick={() => window.electron.maximize()}>
            <Square size={16} />
          </Button>
          <Button variant="subtle" color="gray" size="xs" onClick={() => window.electron.close()}>
            <X size={16} />
          </Button>
        </Group>
      </AppShell.Header>
      <AppShell.Navbar p="md">
        Navbar
        {Array(15)
          .fill(0)
          .map((_, index) => (
            <Skeleton key={index} h={28} mt="sm" animate={false} />
          ))}
      </AppShell.Navbar>
      <AppShell.Main >
        {children}
      </AppShell.Main>
    </AppShell>
  );
}